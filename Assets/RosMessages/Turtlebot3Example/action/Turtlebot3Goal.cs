//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.Turtlebot3Example
{
    [Serializable]
    public class Turtlebot3Goal : Message
    {
        public const string k_RosMessageName = "turtlebot3_example/Turtlebot3";
        public override string RosMessageName => k_RosMessageName;

        //  Define the goal
        public Geometry.Vector3Msg goal;

        public Turtlebot3Goal()
        {
            this.goal = new Geometry.Vector3Msg();
        }

        public Turtlebot3Goal(Geometry.Vector3Msg goal)
        {
            this.goal = goal;
        }

        public static Turtlebot3Goal Deserialize(MessageDeserializer deserializer) => new Turtlebot3Goal(deserializer);

        private Turtlebot3Goal(MessageDeserializer deserializer)
        {
            this.goal = Geometry.Vector3Msg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.goal);
        }

        public override string ToString()
        {
            return "Turtlebot3Goal: " +
            "\ngoal: " + goal.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize, MessageSubtopic.Goal);
        }
    }
}
