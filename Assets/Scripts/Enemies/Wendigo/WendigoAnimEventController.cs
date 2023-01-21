using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoAnimEventController : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private WendigoSFXPlayer sFXPlayer;

    public void Stomp()
    {
        ShakeTheEarth();
        sFXPlayer.PlayStompSFX();
    }

    private void ShakeTheEarth()
    {
        if (PlayerCharacter.Instance.IsGrounded)
        {
            float dist = Vector3.Distance(transform.position, PlayerCharacter.Instance.transform.position);
            ScreenShake.Create().withTime(0.3f).WithAxis(1f, 1f, 0f).WithStrength(0.25f, 2f).WithCurve(4f, 0.1f, 0.25f).WithAttenuation(dist, 200f).Launch();
        }
    }

    public void RoarOnArrival()
    {
        sFXPlayer.PlayArrivalRoarSFX();
    }
}
