using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStageManagerPUN : MonoBehaviour
{
    public static int PlayerStageCounter = 0;
    public static int PlayerHighestStage = 0;
    public static bool EndingFlag = false;
    public static bool IsCleared = false;

    private bool flag = false;

    private int[] stagePositionY;

    private void Start()
    {
        stagePositionY = new int[9] { 0, 24, 48, 72, 96, 120, 144, 168, 200 };
    }


    private void LateUpdate()
    {
        for (int i = 0; i < 8; i++) //8번 (총 8 스테이지)
        {
            if (PlayerControllerPUN.PlayerPosition.y >= stagePositionY[i] && PlayerControllerPUN.PlayerPosition.y < stagePositionY[i + 1] && PlayerControllerPUN.IsJumping == false)
            {
                PlayerStageCounter = i;
                if (PlayerHighestStage < i)
                {
                    PlayerHighestStage = i;
                    TimerControllerPUN.visualizeStageTimerFlag = true;
                    StageCongratulationManagerPUN.visualizeStageCongratulationFlag = true;
                    TimerControllerPUN.visualizeStageTimerArg = i;
                }
            }
        }
        if (PlayerControllerPUN.PlayerPosition.y >= stagePositionY[8] && PlayerControllerPUN.IsJumping == false && flag == false)
        {
            PlayerHighestStage = 8;
            StageCongratulationManagerPUN.visualizeStageCongratulationFlag = true;
            flag = true;
            EndingFlag = true;
            IsCleared = true;
        }
        //Debug.Log(PlayerStageCounter);
    }
}
