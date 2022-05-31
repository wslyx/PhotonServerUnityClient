using Common;
using UnityEngine;
using ExitGames.Client.Photon;
public abstract class Request : MonoBehaviour
{
    public OperationCode opCode;//请求的操作码
    public abstract void DefaultRequest();//默认请求
    public abstract void OnOperationResponse(OperationResponse operationResponse);//取得请求结果

    public virtual void Start()
    {
        PhotonEngine.Instance.AddRequest(this);//所有的请求在PhotonEngine启动时都会添加进单例对象的字典当中
    }

    public virtual void OnDestroy()
    {
        PhotonEngine.Instance.RemoveRequest(this);//PhotonEngine关闭时将请求从字典去除
    }
}
