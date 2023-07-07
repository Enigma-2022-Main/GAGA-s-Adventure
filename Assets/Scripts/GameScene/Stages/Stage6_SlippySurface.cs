using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6_SlippySurface : MonoBehaviour
{
    public static bool IsOnSlippySurface;

    float slippyConstantTemp;

    void Start()
    {
        slippyConstantTemp = PlayerController.SlippyConstant;
    }

    void Update()
    {
        if (FloorCollisionManager.FloorType == "SlippySurface")
        {
            IsOnSlippySurface = true;
            PlayerController.SlippyConstant = 1.0f;
        }
        else
        {
            IsOnSlippySurface = false;
            PlayerController.SlippyConstant = slippyConstantTemp;
        }
    }
}
