using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerPUN : MonoBehaviour
{
    private Vector2 originPosition;

    void Start()
    {
        originPosition = transform.position;
    }

    void LateUpdate()
    {
        if (PlayerControllerPUN.PlayerPosition.y > originPosition.y)
        {
            transform.position = new Vector2(originPosition.x, PlayerControllerPUN.PlayerPosition.y);
        }
        else
        {
            transform.position = originPosition;
        }
    }
}
