using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System;
using Character;

public class User
{
    public string User_Color { get; set; }
    public string Token { get; set; }
    public string Nickname { get; set; }

    public User (string color, string token, string nickname)
    {
        User_Color = color;
        Token = token;
        Nickname = nickname;
    }
}

public class UserManager : MonoBehaviourPunCallbacks
{
    public static UserManager Instance;

    public User user;

    public GameObject[] playerObjects;

    [SerializeField] private GameObject GamePanel;
    [SerializeField] private TMP_Text GameText;

    private bool GameEnd = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Playerinfo()
    {
        playerObjects = new GameObject[2];

        PhotonView[] photonplayer = FindObjectsOfType<PhotonView>();

        for (int i = 0; i < photonplayer.Length; i++)
        {
            playerObjects[i] = photonplayer[i].gameObject;
        }
    }

    private void Update()
    {
        if (GameEnd == false)
        {
            CheckPlayersState();
        }
               
    }

    private void CheckPlayersState()
    {
        int alivePlayers = 0;
        GameObject lastAlivePlayer = null;

        for (int i = 0; i < playerObjects.Length; i++)
        {
            GameObject playerObject = playerObjects[i];

            if (playerObject != null)
            {
                Actor actor = playerObject.GetComponent<Actor>();

                if (actor != null && actor.actorState != Actor.ActorState.Dead)
                {
                    alivePlayers++;
                    lastAlivePlayer = playerObject;
                }
            }
        }

        // 한 명만 살아있으면 승리 처리
        if (alivePlayers == 1 && lastAlivePlayer != null)
        {
            DeclareWinner(lastAlivePlayer);
        }
    }

    // 승리 처리
    private void DeclareWinner(GameObject winner)
    {
        PhotonView winnerActor = winner.GetComponent<PhotonView>();

        if (winnerActor != null)
        {
            GamePanel.SetActive(true);
            GameText.text = string.Empty;
            GameText.text = $"게임 승리자 : {winnerActor.Owner.NickName}\n잠시 후 로비로 퇴장됩니다.";
            GameEnd = true;
            StartCoroutine(GameExit());
        }
    }

    private IEnumerator GameExit()
    {
        yield return new WaitForSeconds(3f);

        GamePanel.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;

        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("RoomScene");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PhotonNetwork.LeaveRoom();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public override void OnLeftRoom()
    {
        GameEnd = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("LoginScene");
    }
}

