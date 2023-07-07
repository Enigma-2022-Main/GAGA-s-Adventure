using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomButton : MonoBehaviour
{
    public void onClickCustomButton()
    {
        Debug.Log("CustomButton");
        CustomPopup.IsClickedCustom = true;
    }

    public void onClickXButton()
    {
        CustomPopup.IsClickedX = true;
    }

    public void onClickApplyButton()
    {
        CustomPopup.IsClickedApply = true;
    }

    public void onClickLeftButton()
    {
        CustomPopup.IsClickedLeft = true;
    }

    public void onClickRightButton()
    {
        CustomPopup.IsClickedRight = true;
    }

}
