﻿using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private const float EMISSION_HEIGHT = 100f;

    [SerializeField] private LaserBeamFader fader;
    [SerializeField] private CrateSFXPlayer sFXPlayer;

    private LineRenderer line;
    private UpDirectionFinder upDirFinder;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        upDirFinder = new UpDirectionFinder();
    }

    public void SwitchOn()
    {
        Vector3 upDir = upDirFinder.GetUpwardDirection(transform);
        Vector3 upPos = transform.position + (upDir * EMISSION_HEIGHT);

        line.SetPosition(0, transform.position);
        line.SetPosition(1, upPos);
        line.enabled = true;

        fader.FadeIn();
        sFXPlayer.PlayLightOn();
    }

    public void ResetLaserBeam()
    {
        fader.ResetFade();
        line.enabled = false;
    }

    public void SwitchOff() => fader.FadeOut();
}