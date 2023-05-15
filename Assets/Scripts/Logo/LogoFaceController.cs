﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoFaceController : MonoBehaviour
{
    private LogoPushController pushController;
    private LogoAnimatedPart[] parts;

    private LogoAnimationStages stage;

    private void Awake()
    {
        pushController = GetComponentInChildren<LogoPushController>();
        parts = GetComponentsInChildren<LogoAnimatedPart>();
    }

    public void SetStage(LogoAnimationStages stage)
    {
        if (this.stage != stage){
            this.stage = stage;

            pushController.Push();

            foreach (LogoAnimatedPart part in parts){
                part.SetFrame(stage);
            }
        }
    }
}