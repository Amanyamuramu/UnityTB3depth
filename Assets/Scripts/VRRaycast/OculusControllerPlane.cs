using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OculusControllerPlane : MonoBehaviour
{
    public GameObject objectToPlace; //表示させるオブジェクト
    public LayerMask collisionLayer; //レイキャストが衝突をチェックするレイヤーマスク
    public float maxDistance = 100f; //レイの最大距離

    private OVRInput.Controller controller; //Oculusコントローラーの種類
    private Vector3 objectPosition; //オブジェクトの位置
    public GameObject ray;
    // public string imagepath; //矢印の場所

    private void Awake()
    {
        //オブジェクトの初期位置を設定
        // objectPosition = objectToPlace.transform.position;

        //コントローラーの種類を設定
        if (gameObject.name.Contains("Right"))
        {
            controller = OVRInput.Controller.LTouch;
        }
        else
        {
            controller = OVRInput.Controller.RTouch;
        }
        controller = OVRInput.Controller.RTouch;
    }

    private void Update()
    {

        //レイを飛ばす起点と方向を計算
        Vector3 rayOrigin = ray.transform.position;
        Vector3 rayDirection = ray.transform.forward;

        //レイキャストを実行して衝突したオブジェクトを取得
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxDistance, collisionLayer))
        {
            //衝突したオブジェクトの位置にオブジェクトを表示
            objectToPlace.transform.position = new Vector3(hit.point.x,1f,hit.point.z);

            //スティックの入力値を取得
            Vector2 stickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);

            //ジョイスティックの向きに応じてオブジェクトを回転
            float angle = Mathf.Atan2(stickInput.x, stickInput.y) * Mathf.Rad2Deg;

            objectToPlace.transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));

        }
        if (OVRInput.GetDown(OVRInput.Button.One, controller)){
            Debug.Log(hit.point);
            //ここでデータを送信
        }
    }
}
