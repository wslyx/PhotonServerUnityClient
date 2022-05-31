using Common;
using UnityEngine;
using ExitGames.Client.Photon;
public abstract class Request : MonoBehaviour
{
    public OperationCode opCode;//����Ĳ�����
    public abstract void DefaultRequest();//Ĭ������
    public abstract void OnOperationResponse(OperationResponse operationResponse);//ȡ��������

    public virtual void Start()
    {
        PhotonEngine.Instance.AddRequest(this);//���е�������PhotonEngine����ʱ������ӽ�����������ֵ䵱��
    }

    public virtual void OnDestroy()
    {
        PhotonEngine.Instance.RemoveRequest(this);//PhotonEngine�ر�ʱ��������ֵ�ȥ��
    }
}
