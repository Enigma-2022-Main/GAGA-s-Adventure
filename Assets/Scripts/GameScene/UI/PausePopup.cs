using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopup : MonoBehaviour
{
    public static bool IsClickedPause = false;
    public static bool IsClickedNo = false;

    private bool isActive = false;

    [SerializeField] private GameObject pausePopup;

    private void Start()
    {
        pausePopup.SetActive(false);
        isActive = false;
    }

    private void Update()
    {
        if (IsClickedPause)
        {           
            pausePopup.SetActive(true);
            isActive = true;
            IsClickedPause = false;
        }
        if (IsClickedNo)
        {
            pausePopup.SetActive(false);
            isActive = false;
            IsClickedNo = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isActive)
            {
                pausePopup.SetActive(false);
                isActive = false;
            }
            else
            {
                pausePopup.SetActive(true);
                isActive = true;
            }
        }

        if (!GameManager.IsMultiPlayer)
        {
            if (isActive)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }
}
