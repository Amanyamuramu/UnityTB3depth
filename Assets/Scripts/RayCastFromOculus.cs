using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCastFromOculus : MonoBehaviour
{
    public LayerMask collisionLayer; //レイキャストが衝突をチェックするレイヤーマスク
    public float maxDistance = 100f; //レイの最大距離
    public GameObject ray;//Right anchorを設定

    public Vector3 GetControllerRayPosition(){
        Vector3 rayOrigin = ray.transform.position;
        Vector3 rayDirection = ray.transform.forward;

        //レイキャストを実行して衝突したオブジェクトを取得
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxDistance, collisionLayer))
        {
            var hitPos = hit.point;
            hitPos.y = 0f;
            return hitPos;
        }

        return new Vector3(0f, 0f, 0f); 
    }
}
