using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class RegisterPanel : MonoBehaviour
{
    public GameObject LoginPanel;
    public InputField UsernameIF;
    public InputField PasswordIF;
    public Text HintMessage;
    private RegisterRequest registerRequest;
    
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        LoginPanel.SetActive(true);
        registerRequest = GetComponent<RegisterRequest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRegisterButton()
    {
        registerRequest.Username = this.UsernameIF.text;
        registerRequest.Password = this.PasswordIF.text;
        registerRequest.DefaultRequest();
    }

    public void OnBackPanelButton()
    {
        this.gameObject.SetActive(false);
        LoginPanel.SetActive(true);
    }

    public void OnRegisterResponse(ReturnCode returnCode)
    {
        Debug.Log("ע������յ����");
        if(returnCode==ReturnCode.Success)
        {
            HintMessage.text = "ע��ɹ�";
        }
        else
        {
            HintMessage.text = "ע��ʧ��";
        }
    }
}
