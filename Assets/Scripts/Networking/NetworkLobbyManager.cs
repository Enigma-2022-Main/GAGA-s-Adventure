using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

public class NetworkLobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1.1";
    private string networkLog;

    [SerializeField]
    private TextMeshProUGUI logText;

    public static bool StartConnectToPunMasterServerFlag = false;
    public static string StartConnectToPunMasterServerFlagArg = "";
    public static bool DisconnectFromPunServerFlag = false;

    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
    }


    private void Update()
    {
        if (StartConnectToPunMasterServerFlag)
        {
            StartConnectToPunMasterServerFlag = false;
            StartConnectToPunMasterServer(StartConnectToPunMasterServerFlagArg);
            
            StartConnectToPunMasterServerFlagArg = "";
        }
        if (DisconnectFromPunServerFlag)
        {
            DisconnectFromPunServerFlag = false;
            DisconnectFromPunServer();
            
        }

        logText.text = "현재 상태: " + networkLog + "\n" + PhotonNetwork.NetworkClientState;
        //Debug.Log(PhotonNetwork.NetworkClientState);
    }

    public void StartConnectToPunMasterServer(string nickname)
    {
        PhotonNetwork.LocalPlayer.NickName = nickname;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting To Master Server...");
        networkLog = "마스터 서버에 접속중...";
    }

    public void DisconnectFromPunServer()
    {
        Debug.Log("Disconnecting To Master Server");
        PhotonNetwork.Disconnect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master Server!");
        networkLog = "마스터 서버와 연결됨";

        Debug.Log("Connecting To Room#1...");
        networkLog = "Room#1에 접속중...";
        PhotonNetwork.JoinOrCreateRoom("Room#1", new RoomOptions { MaxPlayers = 20 }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connected To Room#1!");
        networkLog = "방 참가 성공!";
        PhotonNetwork.IsMessageQueueRunning = false;
        GameStageManagerPUN.IsCleared = false;
        SceneManager.LoadScene("GameScene_Multiplayer");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Failed To Connect To Master Server... Check your network status and retry");
        networkLog = "마스터 서버와 연결 실패... 네트워크 상태를 확인한 후 다시 시도해주세요";
    }
}
