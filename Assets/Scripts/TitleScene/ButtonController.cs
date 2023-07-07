using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void onClickPlayButton()
    {
        //Debug.Log("Loading GameScene");
        SceneManager.LoadScene("PlaymenuScene");
    }

    public void onClickCreditButton()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void onClickScoreboardButton()
    {
        SceneManager.LoadScene("ScoreboardScene");
    }

    public void onClickExitButton()
    {
        Application.Quit();
    }
}
