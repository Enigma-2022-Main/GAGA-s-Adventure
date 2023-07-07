using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class StageCongratulationManagerPUN : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Shader shaderGUIText;

    private Image panelImage;
    private TextMeshProUGUI text1;
    private TextMeshProUGUI text2;
    private TextMeshProUGUI text3;

    private string[] subNames = { "지하실", "1층", "2층", "3층", "4층", "5층", "옥상", "더 높은 곳으로" };

    public static bool visualizeStageCongratulationFlag = false;
    public static bool LastStageCongratulationCompleted = false;
    public static bool readyToGameClearSceneByStageCongratulationManager = false;
    public static bool IsClearCongratulated = false;

    [SerializeField]
    private GameObject congratulationDeco;
    [SerializeField]
    private GameObject stageCongratulationUI;

    void Start()
    {
        congratulationDeco.SetActive(false);
        spriteRenderer = congratulationDeco.GetComponent<SpriteRenderer>();
        shaderGUIText = Shader.Find("GUI/Text Shader");
        spriteRenderer.material.shader = shaderGUIText;
        spriteRenderer.color = Color.white;

        stageCongratulationUI.SetActive(false);
        panelImage = stageCongratulationUI.transform.GetChild(0).gameObject.GetComponent<Image>();
        text1 = stageCongratulationUI.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        text2 = stageCongratulationUI.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        text3 = stageCongratulationUI.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();

        visualizeStageCongratulationFlag = false;
    }

    private void Update()
    {
        text3.text = TimerControllerPUN.HighestStageTimerText;
        if (visualizeStageCongratulationFlag)
        {
            visualizeStageCongratulationFlag = false;

            stageCongratulationUI.SetActive(true);

            if (GameStageManagerPUN.IsCleared == true)
            {
                text1.text = "게임 클리어!";
                text2.text = "- 축하합니다! -";
            }
            else
            {
                if (GameStageManagerPUN.PlayerHighestStage != 7)
                    text1.text = (GameStageManagerPUN.PlayerHighestStage + 1).ToString() + "스테이지";
                else
                    text1.text = "마지막 스테이지";
                text2.text = ("- " + subNames[GameStageManagerPUN.PlayerHighestStage] + " -");
            }

            panelImage.color = new Color(0f, 0f, 0f, 0f);
            text1.color = new Color(0xFF, 0xFF, 0xFF, 0f);
            text2.color = new Color(0xFF, 0xFF, 0xFF, 0f);
            text3.color = new Color(0xFF, 0xFF, 0xFF, 0f);
            spriteRenderer.color = Color.white;

            if (!IsClearCongratulated)
            {
                if (GameStageManagerPUN.PlayerHighestStage == 8)
                {
                    IsClearCongratulated = true;
                }
                congratulationDeco.SetActive(true);
                StartCoroutine(visualizeStageCongratulation());
            }
        }
    }

    IEnumerator visualizeStageCongratulation()
    {
        int count = 0;
        while (count < 250)//2.5초
        {
            if (count == 0)
            {
                StartCoroutine(FadeIn());
            }

            if (count == 210)
            {
                StartCoroutine(FadeOut());
            }

            if (count == 248)
            {
                congratulationDeco.SetActive(false);
                stageCongratulationUI.SetActive(false);
                if (GameStageManagerPUN.IsCleared)
                {
                    if (AWSManager.UserData.IsGuest == true)
                    {
                        GameManager.ClearPosition = PlayerControllerPUN.LocalPlayerInstance.transform.position;
                        PhotonNetwork.LeaveRoom();
                        SceneManager.LoadScene("GameClearScene");
                    }
                    else
                        readyToGameClearSceneByStageCongratulationManager = true;
                }
            }
            count += 2;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator FadeIn() // 0.4초
    {
        float fadeCount = 0;
        spriteRenderer.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        panelImage.color = new Color(0f, 0f, 0f, fadeCount * 0.5f);
        text1.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        text2.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        text3.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.05f;
            yield return new WaitForFixedUpdate();
            spriteRenderer.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
            panelImage.color = new Color(0f, 0f, 0f, fadeCount * 0.5f);
            text1.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
            text2.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
            text3.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        }
    }

    IEnumerator FadeOut()
    {
        float fadeCount = 1;
        spriteRenderer.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        panelImage.color = new Color(0f, 0f, 0f, fadeCount * 0.5f);
        text1.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        text2.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        text3.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.05f;
            yield return new WaitForFixedUpdate();
            spriteRenderer.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
            panelImage.color = new Color(0f, 0f, 0f, fadeCount * 0.5f);
            text1.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
            text2.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
            text3.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        }
    }
}
