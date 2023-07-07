using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class GameClearPopupManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerStat;

    [SerializeField]
    private TextMeshProUGUI gotoScoreboardButton;

    private void Start()
    {
        if (GameManager.IsMultiPlayer)
        {
            gotoScoreboardButton.fontSize = 16;
            gotoScoreboardButton.text = "게임으로 돌아가기";
        }
        else
        {
            gotoScoreboardButton.fontSize = 20;
            gotoScoreboardButton.text = "명예의전당";
        }
        Debug.Log("GameClearPopupManagerStart: " + AWSManager.UserData.DBKey);
    }

    private void Awake()
    {
        Debug.Log("GameClearPopupManagerAwake: " + AWSManager.UserData.DBKey);
    }

    private void Update()
    {
        if (GameManager.IsMultiPlayer)
        {
            // 닉네임
            playerStat.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = AWSManager.UserData.DBKey + "님의 기록";
            // 걸린 시간
            playerStat.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "걸린 시간: " + TimerControllerPUN.HighestStageTimerText;
            // 총 점프 수
            playerStat.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "총 점프 수: " + PlayerControllerPUN.JumpCount.ToString();
            // 총 떨어진 수
            playerStat.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "총 떨어진 수: " + PlayerControllerPUN.FallingCount.ToString();
            // 배수관 횟수
            playerStat.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "배수관에 들어간 횟수: " + Stage6_DrainpipePUN.DrainpipeCount.ToString();
        }
        else
        {
            // 닉네임
            playerStat.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = AWSManager.UserData.DBKey + "님의 기록";
            // 걸린 시간
            playerStat.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "걸린 시간: " + TimerController.HighestStageTimerText;
            // 총 점프 수
            playerStat.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "총 점프 수: " + PlayerController.JumpCount.ToString();
            // 총 떨어진 수
            playerStat.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "총 떨어진 수: " + PlayerController.FallingCount.ToString();
            // 배수관 횟수
            playerStat.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "배수관에 들어간 횟수: " + Stage6_Drainpipe.DrainpipeCount.ToString();
        }

    }

    public void onClickGotoMainButton()
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
            Debug.Log("Disconnect to Pun Server...");
            PhotonNetwork.Disconnect();
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
        }
        SceneManager.LoadScene("TitleScene");
    }
    public void onClickGotoScoreboardButton()
    {
        if (GameManager.IsMultiPlayer)
        {
            //PhotonNetwork.JoinRoom("Room#1");
            PhotonNetwork.JoinOrCreateRoom("Room#1", new RoomOptions { MaxPlayers = 20 }, null);
        }
        else
        {
            AWSManager.UserData = new AWSManager.Userdata
            {
                DBKey = "null",
                AuthKey = "nullAuth",
                HighestScore = 0,
                CustomizationData = -1,
                IsGuest = true
            };
            GameStageManager.PlayerHighestStage = 0;
            GameStageManager.PlayerStageCounter = 0;
            GameStageManager.IsCleared = false;
            PlayerController.Stuck = false;
            PlayerController.SlippyConstant = 0.5f;
            PlayerController.PlayerSpeed = 7.0f;
            TimerController.time = new float[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            SceneManager.LoadScene("ScoreboardScene");
        }
    }

    public override void OnJoinedRoom()
    {
        GameStageManagerPUN.IsCleared = true;
        SceneManager.LoadScene("GameScene_Multiplayer");
    }
}
