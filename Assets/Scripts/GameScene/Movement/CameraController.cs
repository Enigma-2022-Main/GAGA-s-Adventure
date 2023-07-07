using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 originPosition;

    void Start()
    {
        originPosition = transform.position;
    }

    void LateUpdate()
    {
        if (PlayerController.PlayerPosition.y > originPosition.y)
        {
            transform.position = new Vector2(originPosition.x, PlayerController.PlayerPosition.y);
        }
        else
        {
            transform.position = originPosition;
        }
    }
}
