using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Common;

public class LoginRequest : Request
{
    [HideInInspector]
    public string Username;
    [HideInInspector]
    public string Password;

    private LoginPanel loginPanel;
    public override void Start()
    {
        base.Start();
        loginPanel = GetComponent<LoginPanel>();
    }
    public override void DefaultRequest()
    {//���������������
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.Username,Username);
        data.Add((byte)ParameterCode.Password,Password);
        PhotonEngine.Peer.OpCustom((byte)opCode, data, true);//�ͻ�����������˷�������
                                                             //customOpCode���������ͣ����Ƽ�ֱ�Ӵ����֣���ö������enum��
                                                             //customOpParameters�����ݵ�������Dictionary<byte, object> ���ͣ�object����������ĸ��ࡣ
                                                             //sendReliable���Ƿ�ʹ���ȶ������ӡ�
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {//��������Ľ��
        loginPanel.OnLoginResponse((ReturnCode)operationResponse.ReturnCode);
    }
}
