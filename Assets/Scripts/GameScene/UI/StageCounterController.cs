using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageCounterController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] stageCounterText;

    private string[] subNames = { "지하실", "1층", "2층", "3층", "4층", "5층", "옥상", "더 높은 곳으로" };

    private void Start()
    {
        stageCounterText[0].text = "1스테이지";
        stageCounterText[1].text = "지하실";
    }

    private void Update()
    {
        if (!GameStageManager.IsCleared)
        {
            if (GameStageManager.PlayerStageCounter != 7)
            {
                stageCounterText[0].fontSize = 14;
                stageCounterText[0].text = ((GameStageManager.PlayerStageCounter + 1).ToString() + "스테이지");
                stageCounterText[1].fontSize = 10;
                stageCounterText[1].text = ("- " + subNames[GameStageManager.PlayerStageCounter] + " -");
            }
            else
            {
                stageCounterText[0].fontSize = 10;
                stageCounterText[0].text = ("마지막 스테이지");
                stageCounterText[1].fontSize = 8;
                stageCounterText[1].text = ("- 더 높은 곳으로 -");
            }
        }
        else
        {
            stageCounterText[0].fontSize = 12;
            stageCounterText[0].text = ("게임 클리어!");
            stageCounterText[1].fontSize = 10;
            stageCounterText[1].text = ("- 축하합니다! -");
        }
    }
}
