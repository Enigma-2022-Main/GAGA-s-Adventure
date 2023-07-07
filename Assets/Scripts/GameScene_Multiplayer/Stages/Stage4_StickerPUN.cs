using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_StickerPUN : MonoBehaviour
{
    public static bool IsOnSticker;

    float speedTemp;

    void Start()
    {
        speedTemp = PlayerControllerPUN.PlayerSpeed;
    }

    void Update()
    {
        if (FloorCollisionManagerPUN.FloorType == "Sticker")
        {
            IsOnSticker = true;        
            PlayerControllerPUN.PlayerSpeed = speedTemp - 4.0f;
        }
        else
        {
            IsOnSticker = false;
            PlayerControllerPUN.PlayerSpeed = speedTemp;
        }
    }
}
