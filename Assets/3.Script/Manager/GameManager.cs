using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class GameManager : NetworkBehaviour, INetworkRunnerCallbacks
{
	public static GameManager Instance { get; private set; }

    public static List<SessionInfo> myList = new List<SessionInfo>(); // 세션 목록

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
            Debug.Log("로비에 성공적으로 참가했습니다.");
        }
        else
        {
            Debug.LogError($"로비 참가 실패: {result.ShutdownReason}");
        }
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {



        if (sessionList == null || sessionList.Count == 0)
        {
            Debug.Log("사용 가능한 세션이 없습니다.");
            CreateNewSession();
        }
        else
        {
            Debug.Log($"사용 가능한 세션 목록이 업데이트되었습니다. {sessionList.Count}개의 세션이 있습니다.");

            for(int i = 0; i < sessionList.Count; i++)
            {
                if(sessionList[i].IsOpen && sessionList[i].MaxPlayers != sessionList[i].PlayerCount)
                {
                    JoinExistingSession(sessionList[i].Name);
                    break;
                }
                else
                {
                    Debug.Log("입장가능한 세션이 없습니다.");
                    Debug.Log("방을 생성합니다.");
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
            Debug.Log($"세션 '{sessionName}'에 성공적으로 참가했습니다.");
        }
        else
        {
            Debug.LogError($"세션 참가 실패: {result.ShutdownReason}");
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
            Debug.Log("새로운 세션 생성 성공");
        }
        else
        {
            Debug.LogError($"새로운 세션 생성 실패: {result.ShutdownReason}");
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
            // 로컬 플레이어 오브젝트 생성
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

