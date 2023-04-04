using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Nav;
using RosMessageTypes.Geometry;

public class OdomToTarget : MonoBehaviour
{
    private ROSConnection ros;
    private Vector3 agentPosition;
    private Quaternion agentQuat;
    [SerializeField] bool showLogOdom;
    [SerializeField] bool showLogAmcl;
    [SerializeField] GameObject targetAgent;//odometoryを反映させるUnityオブジェクト
     [SerializeField] [Range(1,100)]int positionScaling = 1;

     private Vector3 lastPosition = Vector3.zero;
     private Vector3 lastRot = Vector3.zero;
     private bool isRecievingAmclPose;

     int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<OdometryMsg>("odom", OnSubscribeOdom);
        ros.Subscribe<PoseWithCovarianceStampedMsg>("amcl_pose", OnSubscribeAmclPose);
    }

    // Update is called once per frame
    void Update()
    {
       targetAgent.transform.position = agentPosition;
        targetAgent.transform.rotation = agentQuat;
    }

    void OnSubscribeOdom(OdometryMsg msg)
    {
        if(showLogOdom) Debug.Log("Subscribe-ODOM: " + msg.pose.pose.position);

        //processing for recent data
        Vector3 fixedPosition = new Vector3(
            -(float)msg.pose.pose.position.y * positionScaling,
            0f,
            (float)msg.pose.pose.position.x * positionScaling
        );

        Quaternion q = new Quaternion(
            (float)msg.pose.pose.orientation.x,
            (float)msg.pose.pose.orientation.y,
            (float)msg.pose.pose.orientation.z,
            (float)msg.pose.pose.orientation.w
            
        );

        Vector3 eularRot = q.eulerAngles;


        //calculate differences
        Vector3 positonDiff = fixedPosition - lastPosition;
        agentPosition += positonDiff;


        float rotationDiff = -(eularRot.z - lastRot.z);
        agentQuat = Quaternion.Euler(0f, (agentQuat.eulerAngles.y + rotationDiff), 0f);


        lastPosition = fixedPosition;
        lastRot = eularRot;

    }

    void OnSubscribeAmclPose(PoseWithCovarianceStampedMsg msg)
    {
        if(showLogAmcl) Debug.Log("Subscribe-AMCL: " + msg.pose.pose.position);

        Vector3 fixedPosition = new Vector3(
            -(float)msg.pose.pose.position.y * positionScaling,
            0f,
            (float)msg.pose.pose.position.x * positionScaling
        );

        Quaternion q = new Quaternion(
            (float)msg.pose.pose.orientation.x,
            (float)msg.pose.pose.orientation.y,
            (float)msg.pose.pose.orientation.z,
            (float)msg.pose.pose.orientation.w
            
        );

        Vector3 eularRot = q.eulerAngles;

         
        agentPosition = fixedPosition;
        agentQuat = Quaternion.Euler(0f, -eularRot.z, 0f);

        
    }
}

