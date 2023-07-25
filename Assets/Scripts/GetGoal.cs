using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// パブリッシュするゴールを指定するスクリプト
/// </summary>
public class GetGoal : MonoBehaviour
{
    [SerializeField] GameObject _arrowObj;//表示させる矢印のオブジェクト
    [SerializeField] GameObject _cursorObj;//マウスカーソルのオブジェクト

    [SerializeField] GameObject _goalPrefab;//送信するゴールマーカー
    [SerializeField] GoalPublisher _goalPublisher;

    public GetCursorPosition _getCursorPosition;
    public RayCastFromOculus _getLaserPosition;//Oculusコントローラーレイ用設定

    [SerializeField] GameObject _tb3;
    Vector3 _raycastHitPoint;
    Vector3 _raycastHitPointOnButtonDown;
    [NonSerialized] public Vector3 _goalPosition;
    [NonSerialized] public Vector3 _goalRotation; 
    Quaternion _lookRotation;
    bool _isMouseButtonDown;
    bool _isTriggerOn;
    Vector3 _hitPos;
    [SerializeField] MapReader _mapReader;
    
    void Start()
    {
        _goalPublisher.SendInitialPose();
        _arrowObj.SetActive(false);
    }

    void Update()
    {
        // _raycastHitPoint = _getCursorPosition.GetMousePosition();
        _raycastHitPoint = _getLaserPosition.GetControllerRayPosition();
        _cursorObj.transform.position = new Vector3(_raycastHitPoint.x,0.5f,_raycastHitPoint.z);
        _hitPos = _raycastHitPoint;    
        _hitPos.x -= _mapReader._originPos.x;
        _hitPos.y -= _mapReader._originPos.y;
        _hitPos.z -= _mapReader._originPos.z;
        _hitPos.x *= -1f;
        GetGoalPosDir(); 
    }

    void GetGoalPosDir() {
        Vector3 direction = new Vector3(0f,0f,0f);
        // if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            _raycastHitPointOnButtonDown = _raycastHitPoint;
            // 向きを決めるための矢印オブジェクトを表示する
            _arrowObj.SetActive(true);
            _arrowObj.transform.position = _raycastHitPoint;
            _isTriggerOn = true;
        }

        if (_isTriggerOn)
        {
            Vector2 stickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
            var angle = (Mathf.Atan2(stickInput.x, stickInput.y))* Mathf.Rad2Deg;
            direction = new Vector3(0f,angle,0f);
            _arrowObj.transform.rotation = Quaternion.Euler(direction);

        }


        // if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)){
        if(OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            _arrowObj.SetActive(false);
            _goalPosition = new Vector3(_hitPos.x, 0, _hitPos.z);
            _goalRotation = direction;

            //objに格納すると同時に生成
            
            GameObject obj = Instantiate(_goalPrefab, _raycastHitPointOnButtonDown, Quaternion.Euler(_goalRotation));
            // GameObject obj = Instantiate(_goalPrefab, _raycastHit, Quaternion.Euler(_goalRotation));

            _goalPublisher.SendGoal(_goalPosition, _goalRotation);
            _isTriggerOn = false;
        }
    }

}
