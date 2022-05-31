using Common;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterRequest : Request
{
    [HideInInspector]
    public string Username;
    [HideInInspector]
    public string Password;
    
    private RegisterRequest registerRequest;

    private RegisterPanel registerPanel;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        registerPanel = GetComponent<RegisterPanel>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void DefaultRequest()
    {//���������������
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.Username, this.Username);
        data.Add((byte)ParameterCode.Password, this.Password);
        PhotonEngine.Peer.OpCustom((byte)opCode, data, true);//�ͻ�����������˷�������
                                                             //customOpCode���������ͣ����Ƽ�ֱ�Ӵ����֣���ö������enum��
                                                             //customOpParameters�����ݵ�������Dictionary<byte, object> ���ͣ�object����������ĸ��ࡣ
                                                             //sendReliable���Ƿ�ʹ���ȶ������ӡ�
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {//���շ��������ؽ��
        Debug.Log("�յ�ע����:"+operationResponse.ReturnCode);
        registerPanel.OnRegisterResponse((ReturnCode)operationResponse.ReturnCode);
    }
}
