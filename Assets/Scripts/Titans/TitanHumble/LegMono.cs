using UnityEngine;

public class LegMono : MonoBehaviour
{
    [SerializeField] private Sides side;
    private Leg leg;

    public void Initialize(Titan titan, TitanSetup titanSetup)
    {
        LegSetupPack legSetup = CreateSetupPack(titan, titanSetup);

        leg = new Leg(legSetup);
        titan.movementController.AddLeg(leg);
    }

    private LegSetupPack CreateSetupPack(Titan titan, TitanSetup titanSetup)
    {
        LegSetupPack legSetup = new LegSetupPack();

        legSetup.side = side;
        legSetup.titanTransform = titan.transform;
        legSetup.transform = new TransformProxy(transform);

        legSetup.speed = titanSetup.speed;
        legSetup.stepDistance = titanSetup.stepDistance;
        legSetup.spacing = titanSetup.spacing;
        legSetup.stepHeight = titanSetup.stepHeight;

        return legSetup;
    }
}