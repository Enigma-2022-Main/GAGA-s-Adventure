using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ServerStatusManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI playerCount;
    [SerializeField]
    private TextMeshProUGUI playerNicknames;

    private GameObject[] gameObjects;
    private PhotonView photonViewTmp;

    private string stringTmp;
    private string[] nickNames;

    private bool onPlayerEnteredRoomFlag = false;
    private bool onPlayerLeftRoomFlag = false;

    private void Start()
    {
        nickNames = new string[20];
    }

    private void Update()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Player");
        int i = 0;

        for (int j = 0; j < 20; j++)
        {
            nickNames[j] = null;
        }
        
        foreach (GameObject player in gameObjects)
        {
            photonViewTmp = player.GetComponent<PhotonView>();
            if (i < 9)
            {
                if (photonViewTmp.IsOwnerActive)
                    nickNames[i] = (i + 1) + ". " + photonViewTmp.Owner.NickName;
                else
                {
                    nickNames[i] = "";
                    Destroy(player);
                }
            }
            else if (i == 9)
            {
                nickNames[i] = "...";
            }
            else return;
            i++;
        }

        stringTmp = String.Join("\n", nickNames);

        playerCount.text = PhotonNetwork.CurrentRoom.PlayerCount + " / 20";
        playerNicknames.text = stringTmp;
    }
}
