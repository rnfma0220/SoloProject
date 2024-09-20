using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System;
using UnityEngine.Networking;

[Serializable]
public class RegisterData
{
    public string ID;
    public string Password;
    public string Email;
    public string NickName;
}

public class SignUP : MonoBehaviour
{
    [SerializeField] private TMP_InputField Nickname;
    [SerializeField] private TMP_InputField ID;
    [SerializeField] private TMP_InputField Password;
    [SerializeField] private TMP_InputField Email;
    [SerializeField] private TMP_Text DebugText;

    private class Response
    {
        public string message;
    }

    public void SignUp()
    {
        if(Nickname.text == string.Empty)
        {
            DebugText.text = "닉네임을 입력해주세요.";
        }
        else if(ID.text == string.Empty)
        {
            DebugText.text = "아이디를 입력해주세요.";
        }
        else if (Password.text == string.Empty)
        {
            DebugText.text = "비밀번호를 입력해주세요.";
        }
        else if (Email.text == string.Empty)
        {
            DebugText.text = "이메일을 입력해주세요.";
        }
        else
        {
            DebugText.text = string.Empty;
            StartCoroutine(RegisterCoroutine(ID.text, Password.text, Email.text, Nickname.text));
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

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {

                if (request.responseCode == 409)
                {
                    string requesttext = request.downloadHandler.text;
                    string message = JsonUtility.FromJson<Response>(requesttext).message;
                    DebugText.text = message;
                }
                else
                {
                    DebugText.text = "데이터베이스 연결에 오류가 발생하였습니다.";
                }
            }
            else
            {
                string requesttext = request.downloadHandler.text;
                string message = JsonUtility.FromJson<Response>(requesttext).message;
                DebugText.text = message;
            }
        }
    }

}
