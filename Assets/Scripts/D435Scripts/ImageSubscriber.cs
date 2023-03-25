using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;

public class ImageSubscriber : MonoBehaviour
{
    public Renderer targetRenderer;
    public string topicName;
    private ROSConnection ros;
    private Texture2D tex;

    void Start()
    {
        ros = ROSConnection.instance;
        ros.Subscribe<ImageMsg>(topicName, ReceiveMsg);

        // テクスチャを初期化
        tex = new Texture2D(640, 480, TextureFormat.RGB24, false);
        // tex = new Texture2D(640, 480, TextureFormat.R16, false);
    }

    void ReceiveMsg(ImageMsg msg)
    {
        // バイト列を取り出す
        byte[] imageData = msg.data;
        RenderTexture(imageData);
    }

    private void RenderTexture(byte[] data)
    {        
        // バイト列からtexture2dを生成
        tex.LoadRawTextureData(data);
        
        // テクスチャ割当
        targetRenderer.material.mainTexture = tex;
        tex.Apply();
    }
}
