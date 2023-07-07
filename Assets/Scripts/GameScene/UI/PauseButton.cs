using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PauseButton : MonoBehaviour
{
    public void onClickPauseButton()
    {
        Debug.Log("PauseButton");
        PausePopup.IsClickedPause = true;
    }

    public void onClickYesButton()
    {
        AWSManager.UserData = new AWSManager.Userdata
        {
            DBKey = "null",
            AuthKey = "nullAuth",
            HighestScore = 0,
            CustomizationData = -1,
            IsGuest = true
        };
        if (GameManager.IsMultiPlayer)
        {
            GameStageManagerPUN.PlayerHighestStage = 0;
            GameStageManagerPUN.PlayerStageCounter = 0;
            GameStageManagerPUN.IsCleared = false;
            PlayerControllerPUN.Stuck = false;
            PlayerControllerPUN.SlippyConstant = 0.5f;
            PlayerControllerPUN.PlayerSpeed = 7.0f;
            PlayerControllerPUN.JumpCount = 0;
            PlayerControllerPUN.FallingCount = 0;
            Stage6_DrainpipePUN.DrainpipeCount = 0;
            TimerControllerPUN.time = new float[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            StageCongratulationManagerPUN.IsClearCongratulated = false;
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("TitleScene");
        }
        else
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

            SceneManager.LoadScene("TitleScene");
        }
    }

    public void onClickNoButton()
    {
        PausePopup.IsClickedNo = true;
    }
}
