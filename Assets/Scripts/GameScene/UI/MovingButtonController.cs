using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingButtonController : MonoBehaviour
{
    [SerializeField]
    GameObject toggleButton;
    [SerializeField]
    GameObject MovingButtons;

    Toggle toggle;

    private void Start()
    {
        toggle = toggleButton.GetComponent<Toggle>();
        MovingButtons.SetActive(true);
    }

    public void onLeftButtonDown()
    {
        if (!PlayerController.Stuck)
            PlayerController.ForceLeft = true;
    }

    public void onLeftButtonUp()
    {
        if (!PlayerController.Stuck)
            PlayerController.ForceLeft = false;
    }

    public void onRightButtonDown()
    {
        if (!PlayerController.Stuck)
            PlayerController.ForceRight = true;
    }

    public void onRightButtonUp()
    {
        if (!PlayerController.Stuck)
            PlayerController.ForceRight = false;
    }

    public void onJumpButtonDown()
    {
        if (!PlayerController.Stuck)
            PlayerController.ForceJump = true;
    }

    public void onJumpButtonUp()
    {
        if (!PlayerController.Stuck)
            PlayerController.ForceJump = false;
    }

    public void onClickMovingButtonToggle()
    {
        if (toggle.isOn)
        {
            MovingButtons.SetActive(true);
        }
        else
        {
            MovingButtons.SetActive(false);
        }
    }
}
