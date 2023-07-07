using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartPopup : MonoBehaviour
{
    public static bool IsClickedRestart = false;
    public static bool IsClickedNo = false;

    private bool isActive = false;

    [SerializeField] private GameObject restartPopup;

    private void Start()
    {
        restartPopup.SetActive(false);
        isActive = false;
    }

    private void Update()
    {
        if (IsClickedRestart)
        {
            restartPopup.SetActive(true);
            isActive = true;
            IsClickedRestart = false;
        }
        if (IsClickedNo)
        {
            restartPopup.SetActive(false);
            isActive = false;
            IsClickedNo = false;
        }
    }
}
