using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(SpriteRenderer))]
public class SyncPlayerFlip : MonoBehaviour, IPunObservable
{
	private SpriteRenderer m_spriteRenderer;
	private PhotonView m_view;

	private void Start()
	{
		m_spriteRenderer = GetComponent<SpriteRenderer>();
		m_view = GetComponent<PhotonView>();
		AddObservable();
	}

	private void AddObservable()
	{
		if (!m_view.ObservedComponents.Contains(this))
		{
			m_view.ObservedComponents.Add(this);
		}
	}

	private void Update()
	{
		if (!m_view.IsMine) return;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(m_spriteRenderer.flipX);
		}
		else
		{
			m_spriteRenderer.flipX = (bool)stream.ReceiveNext();
		}
	}
}