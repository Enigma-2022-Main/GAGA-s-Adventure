using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6_SlippySurfacePUN : MonoBehaviour
{
    public static bool IsOnSlippySurface;

    float slippyConstantTemp;

    void Start()
    {
        slippyConstantTemp = PlayerControllerPUN.SlippyConstant;
    }

    void Update()
    {
        if (FloorCollisionManagerPUN.FloorType == "SlippySurface")
        {
            IsOnSlippySurface = true;
            PlayerControllerPUN.SlippyConstant = 1.0f;
        }
        else
        {
            IsOnSlippySurface = false;
            PlayerControllerPUN.SlippyConstant = slippyConstantTemp;
        }
    }
}
