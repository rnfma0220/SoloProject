using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;
using TMPro;

public class GameManager : MonoBehaviour
{
    private NetworkRunner runner;
    public TMP_InputField Roominput;

    private void Start()
    {
        runner = gameObject.AddComponent<NetworkRunner>();
    }

    public void CreateRoom()
    {
        StartGameArgs args = new StartGameArgs()
        {
            GameMode = GameMode.Server,
            SessionName = Roominput.text == string.Empty ? "GameRoom" + Random.Range(0, 100) : Roominput.text,
            PlayerCount = 4,
            Scene = SceneRef.FromIndex(2)
        };

        runner.StartGame(args);
    }

}
