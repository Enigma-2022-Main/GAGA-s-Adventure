using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSyncPosition : MonoBehaviour, IPunObservable
{
    PhotonView photonView;
    Transform tr;
    SpriteRenderer spriteRenderer;
    Vector3 currPos;
    bool currFlipX;

    private void Start()
    {
        tr = gameObject.GetComponent<Transform>();
        photonView = gameObject.GetComponent<PhotonView>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            tr.position = currPos;
            spriteRenderer.flipX = currFlipX;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(tr.position);
            stream.SendNext(spriteRenderer.flipX);
        }
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currFlipX = (bool)stream.ReceiveNext();
            
        }
    }
}
