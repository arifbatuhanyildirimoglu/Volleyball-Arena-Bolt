using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : GlobalEventListener
{
    public void OnCreateServerClicked()
    {
        BoltLauncher.StartServer();
    }

    public void OnCreateClientClicked()
    {
        BoltLauncher.StartClient();
    }

    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            CreateRoom();
        }
        else
        {
            JoinRoom();
        }
    }

    void CreateRoom()
    {
        if (BoltNetwork.IsRunning && BoltNetwork.IsServer)
        {
            BoltMatchmaking.CreateSession("Room1", sceneToLoad: "GameScene");
        }
        else
        {
            Debug.Log("Server not connected");
        }

    }

    void JoinRoom()
    {
        if (BoltNetwork.IsRunning && BoltNetwork.IsClient)
        {
            BoltMatchmaking.JoinSession("Room1");
        }
        else
        {
            Debug.Log("client Not connected");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
