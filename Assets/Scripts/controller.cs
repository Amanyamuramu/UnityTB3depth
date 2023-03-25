using UnityEngine;
using System.Collections;
using System.Collections;
using System.Collections.Generic;using System;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;

public class controller : MonoBehaviour
{

    //Oculus Touch Information
    public OVRInput.Controller controlL;
    public OVRInput.Controller controlR;
    public Rigidbody cube;


    ROSConnection ros;

    //Twist
    Vector3Msg linear = new Vector3Msg(0f, 0f, 0f);
    Vector3Msg angular = new Vector3Msg(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.instance;
        ros.RegisterPublisher<TwistMsg>("/cmd_vel");

        cube = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //初期化
        if (Input.GetKey(KeyCode.Space))
        {
            OVRManager.display.RecenterPose();
            linear.x = 0;
            angular.z = 0;
        }


        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickUp))
        {
            Debug.Log("左アナログスティックを上に倒した");
            linear.x =linear.x+ 0.1;
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickDown))
        {
            Debug.Log("左アナログスティックを下に倒した");
            linear.x =linear.x+  (-0.1);
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickLeft))
        {
            Debug.Log("左アナログスティックを左に倒した");
            angular.z = angular.z +0.1;
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickRight))
        {
            Debug.Log("左アナログスティックを右に倒した");
            angular.z = angular.z + (-0.1);
        }

        
        //Send untiy_odom to turtlebot_control
        TwistMsg Twist = new TwistMsg(
               linear,
               angular
            );

        // Finally send the message to server_endpoint.py running in ROS
        ros.Send("/cmd_vel", Twist);
    }
}
