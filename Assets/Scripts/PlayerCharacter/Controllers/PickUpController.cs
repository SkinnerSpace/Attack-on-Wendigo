using System.Collections;
using System;
using UnityEngine;

public class PickUpController : BaseController, IVisionTriggerObserver, IVisionObserver, IInteractor
{
    private MainController main;
    private IKeeper keeper;
    private IInputReader input;

    private IVisionValidator evaluator;
    private VisionTarget currentTarget;
    private BinaryTrigger binaryTrigger;

    private event Action<Transform> onPickableTargetUpdate;

    public override void Initialize(MainController main)
    {
        this.main = main;
        keeper = main.GetController<WeaponKeeper>();
        input = main.InputReader;

        evaluator = new VisionValidator();
        evaluator.AddSample(typeof(IPickable), 3f);

        binaryTrigger = new BinaryTrigger(main.Events, "TargetIsPresent", "TargetIsAbsent");
    }

    public void Subscribe(IPickUpObserver observer) => onPickableTargetUpdate += observer.OnTargetUpdate;
    public void Unsubscribe(IPickUpObserver observer) => onPickableTargetUpdate -= observer.OnTargetUpdate;

    public override void Connect()
    {
        input.Get<InteractionInputReader>().Subscribe(this);
        main.GetController<VisionDetector>().AddObserver(this);
    }

    public void OnTargetUpdate(VisionTarget newTarget)
    {
        VisionTarget testedTarget = evaluator.Validate(newTarget);

        if (TargetHasChanged(testedTarget))
        {
            currentTarget = testedTarget;
            binaryTrigger.Trigger(currentTarget.IsValid);

            Transform targetObject = (currentTarget.IsValid) ? currentTarget.Transform : null;
            onPickableTargetUpdate?.Invoke(targetObject);
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

public class InteractionController : BaseController, IInteractor, IVisionObserver
{
    private MainController main;
    private IInputReader input;

    private IVisionValidator evaluator;
    private VisionTarget currentTarget;
    private BinaryTrigger binaryTrigger;

    private event Action<Transform> onPickableTargetUpdate;

    public override void Initialize(MainController main)
    {
        this.main = main;
        input = main.InputReader;

        evaluator = new VisionValidator();
        evaluator.AddSample(typeof(IInteractable), 3f);

        binaryTrigger = new BinaryTrigger(main.Events, "TargetIsPresent", "TargetIsAbsent");
    }

    public override void Connect()
    {
        input.Get<InteractionInputReader>().Subscribe(this);
        main.GetController<VisionDetector>().AddObserver(this);
    }

    public void OnTargetUpdate(VisionTarget newTarget)
    {
        VisionTarget testedTarget = evaluator.Validate(newTarget);

        if (TargetHasChanged(testedTarget))
        {
            currentTarget = testedTarget;
            binaryTrigger.Trigger(currentTarget.IsValid);

            Transform targetObject = (currentTarget.IsValid) ? currentTarget.Transform : null;
            onPickableTargetUpdate?.Invoke(targetObject);
        }
    }

    private bool TargetHasChanged(VisionTarget testedTarget) => currentTarget.IsValid != testedTarget.IsValid || currentTarget.type != testedTarget.type;

    public void Interact()
    {
        throw new NotImplementedException();
    }

    
}
