using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Application.targetFrameRate = 120;
        }
        if (GameManager.IsMultiPlayer)
        {
            GameStageManagerPUN.PlayerHighestStage = 0;
        }
        else
        {
            GameStageManager.PlayerHighestStage = 0;
        }
    }
}