using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;
using Fusion.Sockets;
using System;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] private NetworkRunner runner;
    [SerializeField] private NetworkSceneManagerDefault sceneManager;

    private void Start()
    {
        runner = gameObject.AddComponent<NetworkRunner>();
        sceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>();
    }

    private void StartGame(GameMode mode, string roomName)
    {
        var sceneInfo = new NetworkSceneInfo();
        sceneInfo.AddSceneRef(SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex));

        runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = roomName,
            Scene = sceneInfo,  // 씬 정보 설정
            SceneManager = sceneManager  // 씬 관리자 설정
        });
    }
}
