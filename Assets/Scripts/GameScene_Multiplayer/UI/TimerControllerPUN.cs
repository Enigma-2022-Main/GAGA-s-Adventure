using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerControllerPUN : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] timerText = new TextMeshProUGUI[9];

    public static float[] time = new float[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // 0: 현재 시간, 1부터는 스테이지 수

    private int[] hour = new int[9], min = new int[9], sec = new int[9]; // 0: 현재 시간, 1부터는 스테이지 수
    private string[] textTemp = new string[3] { "", "", "" };

    public static bool visualizeStageTimerFlag = false;
    public static int visualizeStageTimerArg = 0;

    public static string HighestStageTimerText = "--:--:--";

    void Start()
    {
        /*
        time = new float[8] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
        hour = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        min = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        sec = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };*/
        if (GameStageManagerPUN.IsCleared)
        {
            restoreTimerData();
            return;
        }
        timerText[0].text = "00:00:00";
        timerText[1].text = "--:--:--";
        timerText[2].text = "--:--:--";
        timerText[3].text = "--:--:--";
        timerText[4].text = "--:--:--";
        timerText[5].text = "--:--:--";
        timerText[6].text = "--:--:--";
        timerText[7].text = "--:--:--";
        timerText[8].text = "--:--:--";

        visualizeStageTimerFlag = false;
    }


    private void restoreTimerData()
    {
        for (int i = 0; i < 8; i++)
        {
            visualizeStageTimer(i);
        }
    }

    void Update()
    {
        HighestStageTimerText = timerText[GameStageManagerPUN.PlayerHighestStage].text;
        if (GameStageManagerPUN.IsCleared == false)
        {
            time[0] += Time.deltaTime;
            hour[0] = (int)time[0] / 3600;
            min[0] = (int)time[0] / 60 % 60;
            sec[0] = (int)time[0] % 60;

            //Debug.Log(time);

            if (hour[0] == 0)
            {
                textTemp[0] = "00";
            }
            else if (hour[0] < 10)
            {
                textTemp[0] = "0" + hour[0].ToString();
            }
            else textTemp[0] = hour[0].ToString();

            if (min[0] == 0)
            {
                textTemp[1] = "00";
            }
            else if (min[0] < 10)
            {
                textTemp[1] = "0" + min[0].ToString();
            }
            else textTemp[1] = min[0].ToString();

            if (sec[0] == 0)
            {
                textTemp[2] = "00";
            }
            else if (sec[0] < 10)
            {
                textTemp[2] = "0" + sec[0].ToString();
            }
            else textTemp[2] = sec[0].ToString();

            timerText[0].text = (textTemp[0].ToString() + ":" + textTemp[1].ToString() + ":" + textTemp[2].ToString());

            if (visualizeStageTimerFlag)
            {
                time[visualizeStageTimerArg] = time[0];
                visualizeStageTimer(visualizeStageTimerArg);
                visualizeStageTimerFlag = false;
            }
        }
        else
        {
            time[8] = time[0];
            visualizeStageTimer(8);
        }
    }
    
    //GameStageManager에서 PlayerHighestStage가 바뀔시 호출

    private void visualizeStageTimer(int stage)
    {
        hour[stage] = (int)time[stage] / 3600;
        min[stage] = (int)time[stage] / 60 % 60;
        sec[stage] = (int)time[stage] % 60;

        if (hour[stage] == 0)
        {
            textTemp[0] = "00";
        }
        else if (hour[stage] < 10)
        {
            textTemp[0] = "0" + hour[stage].ToString();
        }
        else textTemp[0] = hour[stage].ToString();

        if (min[stage] == 0)
        {
            textTemp[1] = "00";
        }
        else if (min[stage] < 10)
        {
            textTemp[1] = "0" + min[stage].ToString();
        }
        else textTemp[1] = min[stage].ToString();

        if (sec[stage] == 0)
        {
            textTemp[2] = "00";
        }
        else if (sec[stage] < 10)
        {
            textTemp[2] = "0" + sec[stage].ToString();
        }
        else textTemp[2] = sec[stage].ToString();

        //Debug.Log(time[stage]);
        timerText[stage].text = (textTemp[0].ToString() + ":" + textTemp[1].ToString() + ":" + textTemp[2].ToString());
    }
}
