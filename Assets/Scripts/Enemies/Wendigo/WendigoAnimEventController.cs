using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoAnimEventController : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private WendigoSFXPlayer sFXPlayer;
    [SerializeField] private Wendigo wendigo;
    [SerializeField] private FireballSpawner fireballSpawner;

    [Header("Stomp")]
    [SerializeField] private StompDamageBox leftStompBox;
    [SerializeField] private StompDamageBox rightStompBox;

    private WendigoData data;

    private void Start()
    {
        data = wendigo.Data;
    }

    public void Stomp()
    {
        ShakeTheEarth();
        sFXPlayer.PlayStompSFX();
    }

    public void LeftStomp()
    {
        leftStompBox.Activate();
        ShakeTheEarth();
        sFXPlayer.PlayStompSFX();
    }

    public void RightStomp()
    {
        rightStompBox.Activate();
        ShakeTheEarth();
        sFXPlayer.PlayStompSFX();
    }

    private void ShakeTheEarth()
    {
        if (CharacterData.Instance.IsGrounded)
        {
            float dist = Vector3.Distance(transform.position, CharacterData.Instance.Position);
            ScreenShake.Create().withTime(0.3f).WithAxis(1f, 1f, 0f).WithStrength(0.25f, 2f).WithCurve(4f, 0.1f, 0.25f).WithAttenuation(dist, 200f).Launch();
        }
    }

    public void StartCastingFireball()
    {
        fireballSpawner.PlayCastVFX();
        sFXPlayer.PlayFireballCastSFX(fireballSpawner.transform.position);
    }
    public void SpawnFireball() => fireballSpawner.SpawnFireball();
    public void FireballCastIsOver() => data.FireballCastIsOver = true;

    public void RoarOnArrival()
    {
        sFXPlayer.PlayArrivalRoarSFX();
        sFXPlayer.PlayArrivalBoneCrackSFX();
    }
}
