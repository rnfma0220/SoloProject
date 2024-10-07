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
        if (PhotonNetwork.IsMasterClient || GameEnd == false)
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
            Debug.Log("Winner is: " + winnerActor.Owner.NickName);
            GameEnd = true;
        }
    }

}

