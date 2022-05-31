using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    void Start()
    {//第一帧更新前调用
        
    }

    void Update()
    {//每帧调用
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("发送请求");
            SendRequest();
        }
    }

    void SendRequest()
    {//按b键时调用
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add(1, 100);
        data.Add(2, "hello server,it's client");
        PhotonEngine.Peer.OpCustom(1, data, true);
    }
}
