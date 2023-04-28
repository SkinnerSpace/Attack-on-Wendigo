using UnityEngine;

public class CrateSweeper : MonoBehaviour, ISwitchable
{
    private const float RADIUS_MODIFIER = 5f;

    [SerializeField] private Crate crate;
    [SerializeField] private CrateLandingController landingController;

    private float radius = 450f;
    private Vector3 center;

    private bool isActive;
    private bool wasUpdated;

    private void Start()
    {
        GameEvents.current.onBlizzardRadiusUpdate += UpdateRadius;
        landingController.onLanded += SwitchOn;
    }

    private void Update()
    {
        if (isActive && wasUpdated && IsOutsideTheBoundaries()){
            crate.Unpack();
        }
    }

    private bool IsOutsideTheBoundaries() => Vector2.Distance(transform.position.FlatV2(), center.FlatV2()) > radius;

    private void UpdateRadius(float radius, Vector3 center)
    {
        wasUpdated = true;

        this.radius = radius - RADIUS_MODIFIER;
        this.center = center;
    }

    public void SwitchOn(){
        isActive = true;
    }

    public void SwitchOff(){
        isActive = false;
    }
}