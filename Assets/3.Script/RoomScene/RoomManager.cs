using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


[System.Serializable]
public class PasswordChangeData
{
    public string oldPassword;
    public string newPassword;
}

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject Changeinfo;
    [SerializeField] private GameObject Setting;

    [SerializeField] private TMP_InputField Cheange_Old;
    [SerializeField] private TMP_InputField Cheange_New;
    [SerializeField] private TMP_Text Password_DebugText;

    private class Response
    {
        public string message;
    }

    public void PasswordChange_Btu()
    {
        if (Setting.activeSelf) Setting.SetActive(false);

        Changeinfo.SetActive(true);
    }

    public void Setting_Btu()
    {
        if (Changeinfo.activeSelf) Changeinfo.SetActive(false);

        Setting.SetActive(true);
    }

    public void JoinandCreate()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("���̾����ϴ�. ���������մϴ�.");
        CreateRoom();
    }

    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4; // �ִ� �÷��̾� �� ����
        PhotonNetwork.CreateRoom(null, options); // �� �̸��� null�� �����ϸ� ������ �̸��� �ο��˴ϴ�.
    }

    public override void OnJoinedRoom()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "�濡 �����Ͽ����ϴ�.");
        SceneManager.LoadScene("Char");
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PhotonNetwork.Instantiate("actor_humanoid", Vector3.zero, Quaternion.identity);

        SceneManager.sceneLoaded -= SceneLoaded;
    }

    public void PasswordChange()
    {
        Password_DebugText.text = string.Empty;

        if (Cheange_Old.text == string.Empty)
        {
            Password_DebugText.text = "���� ��й�ȣ�� �Է����ּ���.";
        }
        else if (Cheange_New.text == string.Empty)
        {
            Password_DebugText.text = "�����Ͻ� ��й�ȣ�� �Է����ּ���.";
        }
        else
        {
            Password_DebugText.text = string.Empty;
            StartCoroutine(PasswordCoroutine(Cheange_Old.text, Cheange_New.text));
        }
    }

    private IEnumerator PasswordCoroutine(string oldpassword, string newpassword)
    {
        string uri = "https://kiwebmeta7mh.o-r.kr/api/changepassword";

        string token = PlayerPrefs.GetString("playertoken");

        PasswordChangeData data = new PasswordChangeData()
        {
            oldPassword = oldpassword,
            newPassword = newpassword
        };

        string json = JsonUtility.ToJson(data);

        using (UnityWebRequest request = new UnityWebRequest(uri, "POST"))
        {
            byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);

            yield return request.SendWebRequest();

            string requesttext = request.downloadHandler.text;

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                string message = JsonUtility.FromJson<Response>(requesttext).message;
                Password_DebugText.text = message;
            }
            else
            {
                string message = JsonUtility.FromJson<Response>(requesttext).message;
                Password_DebugText.text = message;
            }

        }
    }
}
