using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingButtonControllerPUN : MonoBehaviour
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
        if (!PlayerControllerPUN.Stuck)
            PlayerControllerPUN.ForceLeft = true;
    }

    public void onLeftButtonUp()
    {
        if (!PlayerControllerPUN.Stuck)
            PlayerControllerPUN.ForceLeft = false;
    }

    public void onRightButtonDown()
    {
        PlayerControllerPUN.ForceRight = true;
    }

    public void onRightButtonUp()
    {
        if (!PlayerControllerPUN.Stuck)
            PlayerControllerPUN.ForceRight = false;
    }

    public void onJumpButtonDown()
    {
        if (!PlayerControllerPUN.Stuck)
            PlayerControllerPUN.ForceJump = true;
    }

    public void onJumpButtonUp()
    {
        if (!PlayerControllerPUN.Stuck)
            PlayerControllerPUN.ForceJump = false;
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
