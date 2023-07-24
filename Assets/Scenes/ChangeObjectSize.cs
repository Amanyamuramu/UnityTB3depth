using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjectSize : MonoBehaviour
{
    // Start is called before the first frame update
    public float scaleParam = 0.01f;

    private Vector3 scaleChange;
    public float minDistance = 1.0f;
    public float maxDistance = 5.0f;

    public Transform player;
    public Transform targetObject;

    void Start()
    {
        scaleChange = new Vector3(0.01f, 0.01f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, targetObject.position);

        float weight = (distance - minDistance) / (maxDistance - minDistance);

        Debug.Log("Weight: " + weight);

        scaleParam = (1.7f*(1f-weight) -1.0f) / 0.50f;

        scaleChange = new Vector3(scaleParam, scaleParam, scaleParam);

        // change object size 
        this.transform.localScale += scaleChange*Time.deltaTime;

        Vector3 currentScale = this.transform.localScale;
        if(currentScale.x > 1.7f*(1f-weight)){
            this.transform.localScale = new Vector3(1f,1f,1f);
        }
    }
}
