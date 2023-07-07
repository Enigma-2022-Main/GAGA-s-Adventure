using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{

    [Tooltip("The prefab to use for representing the player")]
    [SerializeField]
    private GameObject playerPrefab;

    public static bool IsMultiPlayer = true;
    public static Vector3 ClearPosition;

    void Awake()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        if (playerPrefab == null)
        { // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.

            Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            if (PlayerControllerPUN.LocalPlayerInstance == null)
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                //Debug.Log("ClearPosition : " + ClearPosition);
                if (GameStageManagerPUN.IsCleared)
                    PhotonNetwork.Instantiate(this.playerPrefab.name, ClearPosition, Quaternion.identity, 0);
                else
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(-10f, 1f, 0f), Quaternion.identity, 0);

            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (cause != DisconnectCause.DisconnectByClientLogic)
        {
            Debug.Log("Failed To Connect To Master Server... Trying to Connect Server Again...");
            PhotonNetwork.ReconnectAndRejoin();
        }
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Connected To Room#1!");
        PhotonNetwork.IsMessageQueueRunning = false;
    }
}
