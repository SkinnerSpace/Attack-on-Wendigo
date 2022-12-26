using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GingerbreadAssembly : TitansAssembly
{
    private Gingerbread gingerbread;

    private GingerbreadData data;
    private ITransformProxy titanTransform;
    private IClock clock;

    private MovementController movementController;

    public override Titan Assemble()
    {
        gingerbread = new Gingerbread();

        gingerbread.data = data;
        gingerbread.transform = titanTransform;
        gingerbread.clock = clock;
        gingerbread.movementController = movementController;

        gingerbread.targetPointer = new TargetPointer(titanTransform);

        return gingerbread;
    }

    public override void CreateCoreComponents(TitanSetup setup, Transform transform)
    {
        data = new GingerbreadData(setup);
        titanTransform = new TransformProxy(transform);
        clock = new Clock();
    }

    public override void SetCoreComponents(TitanData data, ITransformProxy titanTransform, IClock clock)
    {
        this.data = data as GingerbreadData;
        this.titanTransform = titanTransform;
        this.clock = clock;
    }

    public override void CreateMovementController(List<ILeg> legs, Torso torso)
    {
        Rotator rotator = new Rotator(titanTransform, clock, data.rotationSpeed);
        Mover mover = new Mover(titanTransform, clock, data.speed);

        LegsSync legsSync = new LegsSync(legs);
        EquipLegs(legs, legsSync);
        legsSync.AddStepMagnitudeObserver(torso);
        torso.SetPosAndAngleDeviations(data.torsoPosDeviation, data.torsoAngleDeviation);

        movementController = new MovementController(rotator, mover, legsSync, torso);
    }

    public void EquipLegs(List<ILeg> legs, LegsSync legsSync)
    {
        foreach (Leg leg in legs)
        {
            LegRaycaster Raycaster = new LegRaycaster(leg, titanTransform);
            Raycaster.SetSpacingAndStepDistance(data.stepSpacing, data.stepDistance);

            LegEngine Engine = new LegEngine(leg, clock);
            Engine.SetSpeedAndStepHeight(data.speed, data.stepHeight);

            leg.Equip(Raycaster, Engine, legsSync);
        }
    }
}
