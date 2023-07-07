using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStageManager : MonoBehaviour
{
    public static int PlayerStageCounter = 0;
    public static int PlayerHighestStage = 0;
    public static bool EndingFlag = false;
    public static bool IsCleared = false;

    private bool flag = false;

    [SerializeField]
    private int[] stagePositionY;

    private void Start()
    {
        stagePositionY = new int[9] { 0, 24, 48, 72, 96, 120, 144, 168, 200 };
    }


    private void LateUpdate()
    {
        for (int i = 0; i < 8; i++) //8번 (총 8 스테이지)
        {
            if (PlayerController.PlayerPosition.y >= stagePositionY[i] && PlayerController.PlayerPosition.y < stagePositionY[i + 1] && PlayerController.IsJumping == false)
            {
                PlayerStageCounter = i;
                if (PlayerHighestStage < i)
                {
                    Debug.Log("Early: " + PlayerHighestStage);
                    PlayerHighestStage = i;
                    Debug.Log("Late: " + PlayerHighestStage);
                    TimerController.visualizeStageTimerFlag = true;
                    StageCongratulationManager.visualizeStageCongratulationFlag = true;
                    TimerController.visualizeStageTimerArg = i;
                }
            }
        }
        if (PlayerController.PlayerPosition.y >= stagePositionY[8] && PlayerController.IsJumping == false && flag == false)
        {
            PlayerHighestStage = 8;
            StageCongratulationManager.visualizeStageCongratulationFlag = true;
            flag = true;
            EndingFlag = true;
            IsCleared = true;
        }

    }
}
