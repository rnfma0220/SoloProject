using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System;
using Photon.Pun;
using Photon.Realtime;
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

public class LoginControll : MonoBehaviourPunCallbacks
{
    #region 회원가입
    [SerializeField] private TMP_InputField SingUP_Nickname;
    [SerializeField] private TMP_InputField SingUP_ID;
    [SerializeField] private TMP_InputField SingUP_Password;
    [SerializeField] private TMP_InputField SingUP_Email;
    [SerializeField] private TMP_Text SingUP_DebugText;
    #endregion

    #region 로그인
    [SerializeField] private TMP_InputField Login_ID;
    [SerializeField] private TMP_InputField Login_Password;
    [SerializeField] private TMP_Text Login_DebugText;
    [SerializeField] private GameObject Loading;
    #endregion

    #region 계정찾기
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
        public string NickName;
        public string PlayerColor;
    }

    public void SignUp()
    {
        SingUP_DebugText.text = string.Empty;

        if (SingUP_Nickname.text == string.Empty)
        {
            SingUP_DebugText.text = "닉네임을 입력해주세요.";
        }
        else if(SingUP_ID.text == string.Empty)
        {
            SingUP_DebugText.text = "아이디를 입력해주세요.";
        }
        else if (SingUP_Password.text == string.Empty)
        {
            SingUP_DebugText.text = "비밀번호를 입력해주세요.";
        }
        else if (SingUP_Email.text == string.Empty)
        {
            SingUP_DebugText.text = "이메일을 입력해주세요.";
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
            Login_DebugText.text = "아이디를 입력해주세요.";
        }
        else if (Login_Password.text == string.Empty)
        {
            Login_DebugText.text = "비밀번호를 입력해주세요.";
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
            account_DebugText.text = "이메일을 입력해주세요.";
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

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                string message = JsonUtility.FromJson<Response>(requesttext).message;
                Login_DebugText.text = message;
            }
            else
            {
                string message = JsonUtility.FromJson<LoginResponse>(requesttext).message;
                string token = JsonUtility.FromJson<LoginResponse>(requesttext).token;
                string nickname = JsonUtility.FromJson<LoginResponse>(requesttext).NickName;
                string playercolor = JsonUtility.FromJson<LoginResponse>(requesttext).PlayerColor;

                UserManager.Instance.user = new User(playercolor, token, nickname);
                Login_DebugText.text = message;
                Loading.SetActive(true);
                PhotonNetwork.ConnectUsingSettings();
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.NickName = UserManager.Instance.user.Nickname;
        SceneManager.LoadScene("RoomScene");
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

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                string message = JsonUtility.FromJson<Response>(requesttext).message;
                SingUP_DebugText.text = message;
            }
            else
            {
                string message = JsonUtility.FromJson<Response>(requesttext).message;
                SingUP_DebugText.text = message;
            }
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

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                string message = JsonUtility.FromJson<Response>(requesttext).message;
                account_DebugText.text = message;
            }
            else
            {
                string message = JsonUtility.FromJson<Response>(requesttext).message;
                account_DebugText.text = message;
            }
        }
    }

}
