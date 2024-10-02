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
    [SerializeField] private GameObject MaMatchmaking;

    [SerializeField] private TMP_InputField Cheange_Old;
    [SerializeField] private TMP_InputField Cheange_New;
    [SerializeField] private TMP_Text Password_DebugText;

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

    public void JoinandCreate()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방이없습니다. 방을생성합니다.");
        CreateRoom();
    }

    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2; // 최대 플레이어 수 설정
        PhotonNetwork.CreateRoom(null, options); // 룸 이름을 null로 설정하면 무작위 이름이 부여됩니다.
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방에 입장했습니다. 현재 인원: " + PhotonNetwork.CurrentRoom.PlayerCount);
        MaMatchmaking.SetActive(true);
        // 방에 입장한 후, 플레이어 수를 확인
        CheckPlayersAndStartGame();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("새로운 플레이어가 입장했습니다. 현재 인원: " + PhotonNetwork.CurrentRoom.PlayerCount);

        // 새로운 플레이어가 입장할 때마다 플레이어 수를 확인
        CheckPlayersAndStartGame();
    }

    private void CheckPlayersAndStartGame()
    {
        // 플레이어 수가 최대값(2명)에 도달하면 씬 이동
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            Debug.Log("모든 플레이어가 입장했습니다. 게임을 시작합니다.");
            MaMatchmaking.SetActive(false);

            // 마스터 클라이언트만 씬을 로드하고, 자동으로 다른 플레이어들에게도 씬을 동기화
            if (PhotonNetwork.IsMasterClient)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                PhotonNetwork.LoadLevel("Char"); // 게임 씬으로 이동
            }
            else
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PhotonNetwork.Instantiate("actor_humanoid", Vector3.zero, Quaternion.identity);
        
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
