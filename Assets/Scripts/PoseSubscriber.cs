using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;

public class PoseSubscriber : MonoBehaviour
{
    [SerializeField] GameObject _tb3;
    Rigidbody _rb;
    ROSConnection _ros;

    void Start()
    {
        _rb = _tb3.GetComponent<Rigidbody>();
        _ros = ROSConnection.GetOrCreateInstance();
        _ros.Subscribe<TwistMsg>("/cmd_vel", MoveTurtlesim);
    }

    void MoveTurtlesim(TwistMsg Msg)
    {      
        var linearVelocity = (float)Msg.linear.x*0.8f;
        var angularVelocity = -1f*(float)Msg.angular.z;
        _rb.velocity = _tb3.transform.rotation * new Vector3(0, 0, linearVelocity);
        _rb.angularVelocity = new Vector3(0, angularVelocity, 0);
    }
}
