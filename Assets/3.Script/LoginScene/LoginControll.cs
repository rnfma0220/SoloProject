using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[Serializable]
public class LoginData
{
    public string ID;
    public string Password;
}

[Serializable]
public class RegisterData
{
    public string ID;
    public string Password;
    public string Email;
    public string NickName;
}

[Serializable]
public class AccountData
{
    public string Email;
}

public class LoginControll : MonoBehaviour
{
    #region ȸ������
    [SerializeField] private TMP_InputField SingUP_Nickname;
    [SerializeField] private TMP_InputField SingUP_ID;
    [SerializeField] private TMP_InputField SingUP_Password;
    [SerializeField] private TMP_InputField SingUP_Email;
    [SerializeField] private TMP_Text SingUP_DebugText;
    #endregion

    #region �α���
    [SerializeField] private TMP_InputField Login_ID;
    [SerializeField] private TMP_InputField Login_Password;
    [SerializeField] private TMP_Text Login_DebugText;
    #endregion

    #region ����ã��
    [SerializeField] private TMP_InputField account_Email;
    [SerializeField] private TMP_Text account_DebugText;
    #endregion

    private class Response
    {
        public string message;
    }

    private class LoginResponse
    {
        public string message;
        public string token;
    }

    public void SignUp()
    {
        SingUP_DebugText.text = string.Empty;

        if (SingUP_Nickname.text == string.Empty)
        {
            SingUP_DebugText.text = "�г����� �Է����ּ���.";
        }
        else if(SingUP_ID.text == string.Empty)
        {
            SingUP_DebugText.text = "���̵� �Է����ּ���.";
        }
        else if (SingUP_Password.text == string.Empty)
        {
            SingUP_DebugText.text = "��й�ȣ�� �Է����ּ���.";
        }
        else if (SingUP_Email.text == string.Empty)
        {
            SingUP_DebugText.text = "�̸����� �Է����ּ���.";
        }
        else
        {
            SingUP_DebugText.text = string.Empty;
            StartCoroutine(RegisterCoroutine(SingUP_ID.text, SingUP_Password.text, SingUP_Email.text, SingUP_Nickname.text));
        }
    }

    public void Login()
    {
        Login_DebugText.text = string.Empty;

        if (Login_ID.text == string.Empty)
        {
            Login_DebugText.text = "���̵� �Է����ּ���.";
        }
        else if (Login_Password.text == string.Empty)
        {
            Login_DebugText.text = "��й�ȣ�� �Է����ּ���.";
        }
        else
        {
            Login_DebugText.text = string.Empty;
            StartCoroutine(LoginCoroutine(Login_ID.text, Login_Password.text));
        }
    }

    public void Account()
    {
        account_DebugText.text = string.Empty;

        if (account_Email.text == string.Empty)
        {
            account_DebugText.text = "�̸����� �Է����ּ���.";
        }
        else
        {
            account_DebugText.text = string.Empty;
            StartCoroutine(AccountCoroutine(account_Email.text));
        }
    }

    private IEnumerator LoginCoroutine(string id, string password)
    {
        string uri = "https://kiwebmeta7mh.o-r.kr/api/login";

        LoginData data = new LoginData()
        {
            ID = id,
            Password = password
        };

        string json = JsonUtility.ToJson(data);

        using (UnityWebRequest request = new UnityWebRequest(uri, "POST"))
        {
            byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            string requesttext = request.downloadHandler.text;
            string message = JsonUtility.FromJson<LoginResponse>(requesttext).message;
            string token = JsonUtility.FromJson<LoginResponse>(requesttext).token;
            PlayerPrefs.SetString("playertoken", token);
            Login_DebugText.text = message;
            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator RegisterCoroutine(string id, string password, string email, string nickname)
    {
        string uri = "https://kiwebmeta7mh.o-r.kr/api/register";

        RegisterData data = new RegisterData()
        {
            ID = id,
            Password = password,
            Email = email,
            NickName = nickname
        };

        string json = JsonUtility.ToJson(data);

        using (UnityWebRequest request = new UnityWebRequest(uri, "POST"))
        {
            byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            string requesttext = request.downloadHandler.text;
            string message = JsonUtility.FromJson<Response>(requesttext).message;
            SingUP_DebugText.text = message;
        }
    }

    private IEnumerator AccountCoroutine(string email)
    {
        string uri = "https://kiwebmeta7mh.o-r.kr/api/account";

        AccountData data = new AccountData()
        {
            Email = email
        };

        string json = JsonUtility.ToJson(data);

        using (UnityWebRequest request = new UnityWebRequest(uri, "POST"))
        {
            byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            string requesttext = request.downloadHandler.text;
            string message = JsonUtility.FromJson<Response>(requesttext).message;

            account_DebugText.text = message;
        }
    }

}