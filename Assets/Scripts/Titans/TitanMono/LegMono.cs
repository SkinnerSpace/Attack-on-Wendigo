using UnityEngine;

public class LegMono : MonoBehaviour
{
    [SerializeField] private Sides side;
    private Leg leg;

    public void Initialize(TitanSetup titanSetup, Titan titan)
    {
        leg = new Leg(side, new TransformProxy(transform));

        LegRaycaster raycaster = new LegRaycaster(leg, titan.transform, titanSetup.spacing, titanSetup.stepDistance);
        leg.SetRaycaster(raycaster);

        LegEngine engine = new LegEngine(leg, titanSetup.speed, titanSetup.stepHeight);
        leg.SetEngine(engine);

        titan.movementController.AddLeg(leg);
    }
}