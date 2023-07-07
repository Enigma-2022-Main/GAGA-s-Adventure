using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_Sticker : MonoBehaviour
{
    public static bool IsOnSticker;

    float speedTemp;

    void Start()
    {
        speedTemp = PlayerController.PlayerSpeed;
    }

    void Update()
    {
        if (FloorCollisionManager.FloorType == "Sticker")
        {
            IsOnSticker = true;        
            PlayerController.PlayerSpeed = speedTemp - 4.0f;
        }
        else
        {
            IsOnSticker = false;
            PlayerController.PlayerSpeed = speedTemp;
        }
    }
}
