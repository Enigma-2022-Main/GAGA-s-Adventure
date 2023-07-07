using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomPopup : MonoBehaviour
{
    public static bool IsClickedCustom = false;
    public static bool IsClickedX = false;
    public static bool IsClickedApply = false;
    public static bool IsClickedLeft = false;
    public static bool IsClickedRight = false;

    private int hatCount = 0;

    [SerializeField] private Sprite[] images;
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Image hatImage;

    [SerializeField] private GameObject customPopup;

    private void Start()
    {
        customPopup.SetActive(false);
    }

    private void Update()
    {
        if (IsClickedCustom)
        {
            customPopup.SetActive(true);
            hatCount = AWSManager.UserData.CustomizationData;
            IsClickedCustom = false;
        }
        if (IsClickedX)
        {
            customPopup.SetActive(false);
            IsClickedX = false;
        }
        if (IsClickedLeft)
        {
            hatCount--;
            IsClickedLeft = false;
        }
        if (IsClickedRight)
        {
            hatCount++;
            IsClickedRight = false;
        }

        if (hatCount <= -2)
        {
            hatCount = -1;
        }
        if (hatCount >= 6)
        {
            hatCount = 5;
        }

        if (hatCount != -1)
            hatImage.sprite = images[hatCount];
        else
            hatImage.sprite = defaultImage;

        if (IsClickedApply)
        {
            //Àû¿ë
            AWSManager.UserData.CustomizationData = hatCount;

            customPopup.SetActive(false);
            IsClickedApply = false;
        }

    }
}
