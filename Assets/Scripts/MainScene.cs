using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    void Start()
    {//��һ֡����ǰ����
        
    }

    void Update()
    {//ÿ֡����
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("��������");
            SendRequest();
        }
    }

    void SendRequest()
    {//��b��ʱ����
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add(1, 100);
        data.Add(2, "hello server,it's client");
        PhotonEngine.Peer.OpCustom(1, data, true);
    }
}
