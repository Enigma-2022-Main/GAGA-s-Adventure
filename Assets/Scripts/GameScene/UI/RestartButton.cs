using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public static bool RestartedFlag = false;
    public void onClickRestartButton()
    {
        Debug.Log("RestartButton");
        RestartPopup.IsClickedRestart = true;
    }

    public void onClickYesButton()
    {
        GameStageManager.PlayerHighestStage = 0;
        GameStageManager.PlayerStageCounter = 0;
        GameStageManager.IsCleared = false;
        PlayerController.Stuck = false;
        PlayerController.SlippyConstant = 0.5f;
        PlayerController.PlayerSpeed = 7.0f;
        PlayerController.JumpCount = 0;
        PlayerController.FallingCount = 0;
        Stage6_Drainpipe.DrainpipeCount = 0;
        TimerController.time = new float[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        GameObject.FindWithTag("Player").transform.position = new Vector3(-10f, 1f, 1f);

        RestartedFlag = true;

        RestartPopup.IsClickedNo = true;
    }

    public void onClickNoButton()
    {
        RestartPopup.IsClickedNo = true;
    }
}
