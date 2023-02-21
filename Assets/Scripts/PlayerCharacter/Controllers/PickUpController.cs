using System.Collections;
using System;
using UnityEngine;

public class PickUpController : BaseController, IVisionTriggerObserver, IVisionObserver, IInteractor
{
    private MainController main;
    private IVisionValidator evaluator;
    private VisionTarget currentTarget;
    private IKeeper keeper;
    private PickUpTriggers triggers;

    public override void Initialize(MainController main)
    {
        this.main = main;
        keeper = main.GetController<WeaponKeeper>();

        evaluator = new VisionValidator();
        evaluator.AddSample(typeof(IPickable), 3f);

        triggers = new PickUpTriggers();
        triggers.SetUp(main.Events);
    }

    public override void Connect()
    {
        MainInputReader.Get<InteractionInputReader>().Subscribe(this);
        main.GetController<VisionDetector>().AddObserver(this);
    }

    public void OnTargetUpdate(VisionTarget newTarget)
    {
        VisionTarget testedTarget = evaluator.Validate(newTarget);

        if (TargetHasChanged(testedTarget))
        {
            currentTarget = testedTarget;
            triggers.Activate(currentTarget.IsValid);
        }
    }

    private bool TargetHasChanged(VisionTarget testedTarget) => currentTarget.IsValid != testedTarget.IsValid || currentTarget.type != testedTarget.type;

    public void Interact()
    {
        if (currentTarget.IsValid)
        {
            keeper.TakeAnItem(currentTarget.Transform.GetComponent<IPickable>());
        }
    }
}
