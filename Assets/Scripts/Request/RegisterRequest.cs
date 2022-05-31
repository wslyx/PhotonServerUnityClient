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
    {//向服务器发送请求
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.Username, this.Username);
        data.Add((byte)ParameterCode.Password, this.Password);
        PhotonEngine.Peer.OpCustom((byte)opCode, data, true);//客户端向服务器端发送请求
                                                             //customOpCode，请求类型，不推荐直接传数字，用枚举类型enum。
                                                             //customOpParameters，传递的数据是Dictionary<byte, object> 类型，object类是所有类的父类。
                                                             //sendReliable，是否使用稳定的连接。
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {//接收服务器返回结果
        Debug.Log("收到注册结果:"+operationResponse.ReturnCode);
        registerPanel.OnRegisterResponse((ReturnCode)operationResponse.ReturnCode);
    }
}
