using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class VisualizePlayerNickname : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nameText;

    PhotonView photonView;

    void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            nameText.text = AWSManager.UserData.DBKey;
            nameText.color = new Color(0, 251, 255, 255);
        }
        else
            nameText.text = photonView.Owner.NickName;
    }
}
