using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Common;

public class PhotonEngine : MonoBehaviour,IPhotonPeerListener
{
    public static PhotonEngine Instance;

    public static PhotonPeer Peer;

    private Dictionary<OperationCode, Request> RequestDict = new Dictionary<OperationCode, Request>();

    public void AddRequest(Request request)
    {
        RequestDict.Add(request.opCode, request);
    }

    public void RemoveRequest(Request request)
    {
        RequestDict.Remove(request.opCode);
    }

    //创建此类实例时调用
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        //通过listener接收服务器的响应
        Peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        Peer.Connect("127.0.0.1:5055", "MyGame1");
    }

    // Update is called once per frame
    void Update()
    {
        Peer.Service();
    }

    void OnDestroy()
    {
        if (Peer != null && Peer.PeerState == PeerStateValue.Connected)
        {
            Peer.Disconnect();
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
    }

    //响应服务器发来的event
    public void OnEvent(EventData eventData)
    {
        switch(eventData.Code)
        {
            case 1:
                Debug.Log("收到服务器端发来的事件");
                Dictionary<byte, object> data = eventData.Parameters;
                object intValue;
                data.TryGetValue(1, out intValue);
                object stringValue;
                data.TryGetValue(2, out stringValue);
                Debug.Log("得到的事件数据是:" + intValue.ToString() + stringValue.ToString() + data.Count.ToString());
                break;
            default:
                break;
        }
    }

    //处理客户端请求时被调用
    public void OnOperationResponse(OperationResponse operationResponse)
    {
        OperationCode opCode = (OperationCode)operationResponse.OperationCode;
        Request request = null;
        bool temp = RequestDict.TryGetValue(opCode, out request);

        if(temp)
        {
            request.OnOperationResponse(operationResponse);
        }
        else
        {
            Debug.Log("没找到对应的响应处理对象");
        }
    }

    //连接状态发生改变时被调用
    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log("当前连接状态:"+statusCode);
    }
}
