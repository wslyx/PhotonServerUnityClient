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

    //��������ʵ��ʱ����
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
        //ͨ��listener���շ���������Ӧ
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

    //��Ӧ������������event
    public void OnEvent(EventData eventData)
    {
        switch(eventData.Code)
        {
            case 1:
                Debug.Log("�յ��������˷������¼�");
                Dictionary<byte, object> data = eventData.Parameters;
                object intValue;
                data.TryGetValue(1, out intValue);
                object stringValue;
                data.TryGetValue(2, out stringValue);
                Debug.Log("�õ����¼�������:" + intValue.ToString() + stringValue.ToString() + data.Count.ToString());
                break;
            default:
                break;
        }
    }

    //����ͻ�������ʱ������
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
            Debug.Log("û�ҵ���Ӧ����Ӧ�������");
        }
    }

    //����״̬�����ı�ʱ������
    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log("��ǰ����״̬:"+statusCode);
    }
}
