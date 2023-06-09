using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry; //「ROS TCP Connector」のRos Messageを使用

public class Tb3cmdSubscriber : MonoBehaviour
{
    public Rigidbody cube;

    void Start()
    {
        ROSConnection.instance.Subscribe<TwistMsg>("/cmd_vel", turtlesim_move);
        print("start cmd_vel connection");
    }

    void turtlesim_move(TwistMsg Msg)
    {
        // print(Msg);
        cube.velocity = transform.forward * (float)Msg.linear.x;
        cube.angularVelocity = new Vector3(0, -(float)Msg.angular.z, 0);
        print(Msg.linear.x);
    }
}