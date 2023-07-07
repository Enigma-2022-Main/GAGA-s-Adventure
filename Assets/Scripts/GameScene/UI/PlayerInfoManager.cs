using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField]
    private GameObject text1GameObject;
    [SerializeField]
    private GameObject text2GameObject;

    private TextMeshProUGUI text1;
    private TextMeshProUGUI text2;

    private float highestTime;
    private string[] textTemp = new string[3] { "", "", "" };

    void Start()
    {
        text1 = text1GameObject.GetComponent<TextMeshProUGUI>();
        text2 = text2GameObject.GetComponent<TextMeshProUGUI>();

        text1.text = AWSManager.UserData.DBKey + "님의 현재기록: ";

        highestTime = AWSManager.UserData.HighestScore;
        int hour = (int)highestTime / 3600;
        int min = (int)highestTime / 60 % 60;
        int sec = (int)highestTime % 60;

        if (hour == 0)
        {
            textTemp[0] = "00";
        }
        else if (hour < 10)
        {
            textTemp[0] = "0" + hour.ToString();
        }
        else textTemp[0] = hour.ToString();

        if (min == 0)
        {
            textTemp[1] = "00";
        }
        else if (min < 10)
        {
            textTemp[1] = "0" + min.ToString();
        }
        else textTemp[1] = min.ToString();

        if (sec == 0)
        {
            textTemp[2] = "00";
        }
        else if (sec < 10)
        {
            textTemp[2] = "0" + sec.ToString();
        }
        else textTemp[2] = sec.ToString();

        if (AWSManager.UserData.IsGuest)
            text2.text = "--:--:--";
        else
            text2.text = (textTemp[0].ToString() + ":" + textTemp[1].ToString() + ":" + textTemp[2].ToString());
    }
}
