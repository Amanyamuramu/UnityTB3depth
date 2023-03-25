using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;

public class DepthSubscriber : MonoBehaviour
{
    // public Renderer targetRenderer;
    public string topicName;
    private ROSConnection ros;
    private Texture2D tex;

    // public int depthWidth = 640;
    // public int depthHeight = 480;
    // public int depthLayers = 1;

    // private Texture3D depthTexture;

    // protected override void ReceiveMessage(MessageTypes.Sensor.CompressedImage message)
    // {
    //     byte[] depthData = message.data;

    //     ushort[] depthValues = new ushort[depthWidth * depthHeight * depthLayers];
    //     int depthIndex = 0;
    //     for (int i = 0; i < depthData.Length; i += 2)
    //     {
    //         ushort depthValue = (ushort)(depthData[i] + depthData[i + 1] * 256);
    //         depthValues[depthIndex] = depthValue;
    //         depthIndex++;
    //     }

    //     if (depthTexture == null)
    //     {
    //         depthTexture = new Texture3D(depthWidth, depthHeight, depthLayers, TextureFormat.R16, false);
    //     }

    //     depthTexture.SetPixels32(ConvertDepthToColor(depthValues));
    //     depthTexture.Apply();
    // }

    // private Color32[] ConvertDepthToColor(ushort[] depthValues)
    // {
    //     Color32[] colors = new Color32[depthValues.Length];

    //     for (int i = 0; i < depthValues.Length; i++)
    //     {
    //         ushort depthValue = depthValues[i];

    //         // 0を最小値とし、最大値までの値域を使ってカラーを作成する。
    //         byte depthByte = (byte)(depthValue / 256.0f * 255.0f);
    //         Color32 color = new Color32(depthByte, depthByte, depthByte, 255);

    //         colors[i] = color;
    //     }

    //     return colors;
    // }

    void Start()
    {
        ros = ROSConnection.instance;
        ros.Subscribe<ImageMsg>(topicName, ReceiveMsg);

        // テクスチャを初期化
        tex = new Texture2D(640, 480, TextureFormat.RGB24, false);
    }

    void ReceiveMsg(ImageMsg msg)
    {
        // バイト列を取り出す
        // byte[] imageData = msg.data;
        byte[] depthData = msg.data;
        Debug.Log(depthData);
        // RenderTexture(imageData);

    }

    private void RenderTexture(byte[] data)
    {        
        // バイト列からtexture2dを生成
        // tex.LoadRawTextureData(data);
        // テクスチャ割当
        // targetRenderer.material.mainTexture = tex;
        // tex.Apply();
    }
}
