using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using UnityEngine.SceneManagement;

public class LoginPanel : MonoBehaviour
{
    public GameObject RegisterPanel;
    public InputField UsernameIF;
    public InputField PasswordIF;

    private LoginRequest loginRequest;
    public Text HintMessage;

    // Start is called before the first frame update
    void Start()
    {
        loginRequest = GetComponent<LoginRequest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRegisterButton()
    {
        this.gameObject.SetActive(false);
        RegisterPanel.SetActive(true);
    }

    public void OnLoginButton()
    {
        loginRequest.Username = UsernameIF.text;
        loginRequest.Password = PasswordIF.text;
        loginRequest.DefaultRequest();
    }

    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {//��ת����һ������
            SceneManager.LoadScene("Game");
        }
        else
        {
            HintMessage.text = "�û������������";
        }
    }
}
