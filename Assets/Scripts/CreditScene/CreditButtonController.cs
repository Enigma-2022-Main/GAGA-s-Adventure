using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditButtonController : MonoBehaviour
{
    public void onClickBackButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void onClickNextPageButton()
    {
        SceneManager.LoadScene("CreditScene_2");
    }
    public void onClickPrevPageButton()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
