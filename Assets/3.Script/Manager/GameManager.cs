using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class GameManager : NetworkBehaviour, INetworkRunnerCallbacks
{
	public static GameManager Instance { get; private set; }

    public static List<SessionInfo> myList = new List<SessionInfo>(); // ���� ���

    [SerializeField] private NetworkRunner RunnerPrefab;
    private NetworkRunner runner;

    public NetworkObject MyPlayerPrefab;


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

    public async void Mama()
    {
        runner = Instantiate(RunnerPrefab);
        runner.ProvideInput = true;

        runner.AddCallbacks(this);

        var result = await runner.JoinSessionLobby(SessionLobby.ClientServer);

        if (result.Ok)
        {
            Debug.Log("�κ� ���������� �����߽��ϴ�.");
        }
        else
        {
            Debug.LogError($"�κ� ���� ����: {result.ShutdownReason}");
        }
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {



        if (sessionList == null || sessionList.Count == 0)
        {
            Debug.Log("��� ������ ������ �����ϴ�.");
            CreateNewSession();
        }
        else
        {
            Debug.Log($"��� ������ ���� ����� ������Ʈ�Ǿ����ϴ�. {sessionList.Count}���� ������ �ֽ��ϴ�.");

            for(int i = 0; i < sessionList.Count; i++)
            {
                if(sessionList[i].IsOpen && sessionList[i].MaxPlayers != sessionList[i].PlayerCount)
                {
                    JoinExistingSession(sessionList[i].Name);
                    break;
                }
                else
                {
                    Debug.Log("���尡���� ������ �����ϴ�.");
                    Debug.Log("���� �����մϴ�.");
                    CreateNewSession();
                }
            }

        }
    }

    private async void JoinExistingSession(string sessionName)
    {
        var result = await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = sessionName
        });

        if (result.Ok)
        {
            Debug.Log($"���� '{sessionName}'�� ���������� �����߽��ϴ�.");
        }
        else
        {
            Debug.LogError($"���� ���� ����: {result.ShutdownReason}");
        }
    }

    private async void CreateNewSession()
    {
        var sceneInfo = new NetworkSceneInfo();
        sceneInfo.AddSceneRef(SceneRef.FromIndex(2));

        var result = await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = "NewRoom_" + UnityEngine.Random.Range(1, 1000),
            Scene = sceneInfo,
            PlayerCount = 4
        });

        if (result.Ok)
        {
            Debug.Log("���ο� ���� ���� ����");
        }
        else
        {
            Debug.LogError($"���ο� ���� ���� ����: {result.ShutdownReason}");
        }
    }

    #region NetworkRunnerCallback

    public void OnConnectedToServer(NetworkRunner runner) { }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

    public void OnInput(NetworkRunner runner, NetworkInput input) { }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.LocalPlayer == player)
        {
            // ���� �÷��̾� ������Ʈ ����
            NetworkObject localPlayerObject = runner.Spawn(MyPlayerPrefab, new Vector3(2.5f, 15f, 0f), Quaternion.identity, player);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }

    public void OnSceneLoadDone(NetworkRunner runner) { }

    public void OnSceneLoadStart(NetworkRunner runner) { }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

    #endregion
}

