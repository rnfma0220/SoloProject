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
#if UNITY_EDITOR
using UnityEditor;
#endif


[System.Serializable]
public class PasswordChangeData
{
    public string oldPassword;
    public string newPassword;
}

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button RoomCreate_Btn;
    [SerializeField] private Button Changeinfo_Btn;
    [SerializeField] private Button Setting_Btn;
    [SerializeField] private Button Logout_Btn;

    [SerializeField] private GameObject Changeinfo;
    [SerializeField] private GameObject Setting;
    [SerializeField] private GameObject MaMatchmaking;

    [SerializeField] private TMP_InputField Cheange_Old;
    [SerializeField] private TMP_InputField Cheange_New;
    [SerializeField] private TMP_Text Password_DebugText;

    private Vector3 spawnpoint;
    private Quaternion spawnquat;

    private class Response
    {
        public string message;
    }

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
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

    public void Logout_Btu()
    {
        PhotonNetwork.Disconnect();
    }

    public void Matchmaking_Close()
    {
        MaMatchmaking.SetActive(false);
        RoomCreate_Btn.interactable = true;
        Changeinfo_Btn.interactable = true;
        Setting_Btn.interactable = true;
        Logout_Btn.interactable = true;
        PhotonNetwork.LeaveRoom();
    }

    public void JoinandCreate()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2; // 최대 플레이어 수 설정
        PhotonNetwork.CreateRoom(null, options);
    }

    public override void OnJoinedRoom()
    {
        MaMatchmaking.SetActive(true);
        RoomCreate_Btn.interactable = false;
        Changeinfo_Btn.interactable = false;
        Setting_Btn.interactable = false;
        Logout_Btn.interactable = false;
        CheckPlayersAndStartGame();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        CheckPlayersAndStartGame();
    }

    private void CheckPlayersAndStartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            MaMatchmaking.SetActive(false);

            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                SceneManager.sceneLoaded += OnSceneLoaded;
                PhotonNetwork.LoadLevel("GameScene");
            }
            else
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        if (playerIndex == 0)
        {
            spawnpoint = new Vector3(6.3f, 5f, -6.3f);
            spawnquat = Quaternion.identity;
        }
        else
        {
            spawnpoint = new Vector3(6.3f, 5f, 11.3f);
            spawnquat = Quaternion.Euler(0, 180, 0);
        }

        UserManager.Instance.playerObjects = new GameObject[PhotonNetwork.PlayerList.Length];

        GameObject playerinfo =  PhotonNetwork.Instantiate("actor_humanoid", spawnpoint, spawnquat);

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void PasswordChange()
    {
        Password_DebugText.text = string.Empty;

        if (Cheange_Old.text == string.Empty)
        {
            Password_DebugText.text = "기존 비밀번호를 입력해주세요들송.";
        }
        else if (Cheange_New.text == string.Empty)
        {
            Password_DebugText.text = "변경하실 비밀번호를 입력해주세요.";
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

        string token = UserManager.Instance.user.Token;

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
