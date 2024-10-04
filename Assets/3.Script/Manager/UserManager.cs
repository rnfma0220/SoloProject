using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public GameObject[] playerObjects = new GameObject[2];

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
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonView[] photonplayer = FindObjectsOfType<PhotonView>();

            for (int i = 0; i < photonplayer.Length; i++)
            {
                playerObjects[i] = photonplayer[i].gameObject;
            }
        }
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            CheckPlayersState();
        }
    }

    // 플레이어 상태 체크하여 승리자 처리
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

                // Actor 상태가 Dead가 아니면 살아있는 플레이어로 간주
                if (actor != null && actor.actorState != Actor.ActorState.Dead)
                {
                    alivePlayers++;
                    lastAlivePlayer = playerObject; // 마지막으로 살아남은 플레이어 저장
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
            Debug.Log("Winner is: " + winnerActor.Owner.NickName);

        }
    }

}

