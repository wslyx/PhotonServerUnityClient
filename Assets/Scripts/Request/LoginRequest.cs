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
    {//向服务器发送请求
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.Username,Username);
        data.Add((byte)ParameterCode.Password,Password);
        PhotonEngine.Peer.OpCustom((byte)opCode, data, true);//客户端向服务器端发送请求
                                                             //customOpCode，请求类型，不推荐直接传数字，用枚举类型enum。
                                                             //customOpParameters，传递的数据是Dictionary<byte, object> 类型，object类是所有类的父类。
                                                             //sendReliable，是否使用稳定的连接。
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {//接收请求的结果
        loginPanel.OnLoginResponse((ReturnCode)operationResponse.ReturnCode);
    }
}
