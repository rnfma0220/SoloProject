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
            Scene = sceneInfo,  // �� ���� ����
            SceneManager = sceneManager  // �� ������ ����
        });
    }
}
