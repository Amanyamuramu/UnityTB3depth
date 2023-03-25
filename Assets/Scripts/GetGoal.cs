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
    // [SerializeField] TextMeshProUGUI _cursorPositionText;
    // [SerializeField] TextMeshProUGUI _goalPosRotText;
    public GetCursorPosition _getCursorPosition;
    // [SerializeField] GetCursorPosition _getCursorPosition;
    [SerializeField] GameObject _tb3;
    Vector3 _raycastHitPoint;
    Vector3 _raycastHitPointOnButtonDown;
    [NonSerialized] public Vector3 _goalPosition;
    [NonSerialized] public Vector3 _goalRotation; 
    Quaternion _lookRotation;
    bool _isMouseButtonDown;
    Vector3 _hitPos;
    [SerializeField] MapReader _mapReader;
    
    void Start()
    {
        _arrowObj.SetActive(false);

        // _cursorPositionText.text = "Cursor: ";
        // _goalPosRotText.text = "Goal: ";
    }

    void Update()
    {
        //ここにオキュラスとの当たり判定からVector3型を作成する
        _raycastHitPoint = _getCursorPosition.GetMousePosition();

        _cursorObj.transform.position = _raycastHitPoint;

        // マップ用に座標の基準点を変換する
        _hitPos = _raycastHitPoint;    
        _hitPos.x -= _mapReader._originPos.x;
        _hitPos.y -= _mapReader._originPos.y;
        _hitPos.z -= _mapReader._originPos.z;
        _hitPos.x *= -1f;

        // _cursorPositionText.text = "Cursor: " + _hitPos.ToString();
        
        GetGoalPosDir(); 
    }

    void GetGoalPosDir() {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (_isMouseButtonDown) return;

            _raycastHitPointOnButtonDown = _raycastHitPoint;

            // 向きを決めるための矢印オブジェクトを表示する
            _arrowObj.SetActive(true);
            _arrowObj.transform.position = _raycastHitPoint;

            _isMouseButtonDown = true;
        }
        

        if (_isMouseButtonDown)
        {
            var direction = _raycastHitPoint - _arrowObj.transform.position;
            direction.y = 0;
            _lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            _arrowObj.transform.rotation = Quaternion.Lerp(_arrowObj.transform.rotation, _lookRotation, 0.1f);
        }


        if (Input.GetMouseButtonUp(0))
        {
            if (!_isMouseButtonDown) return;

            _arrowObj.SetActive(false);

            _goalPosition = new Vector3(_hitPos.x, 0, _hitPos.z);
            _goalRotation = Quaternion.Lerp(_arrowObj.transform.rotation, _lookRotation, 0.1f).eulerAngles;

            //objに格納すると同時に生成
            GameObject obj = Instantiate(_goalPrefab, _raycastHitPointOnButtonDown, Quaternion.Euler(_goalRotation));

            _goalPublisher.SendGoal(_goalPosition, _goalRotation);
            //rayから取得されたpositionとrotationをパブリッシュする

            _isMouseButtonDown = false;
        }
    }

}
