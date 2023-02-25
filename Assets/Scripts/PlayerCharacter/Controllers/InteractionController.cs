using System;
using UnityEngine;

public class InteractionController : BaseController, IInteractor, IVisionObserver
{
    private MainController main;
    private IInputReader input;
    private VisionTargetProcessor targetProcessor;

    private VisionTarget currentTarget;
    private event Action<Transform> onInteractableTargetUpdate;

    public override void Initialize(MainController main)
    {
        this.main = main;
        input = main.InputReader;

        IVisionValidator validator = new VisionValidator();
        validator.AddSample(typeof(IInteractable), 3f);

        BinaryTrigger binaryTrigger = new BinaryTrigger(main.Events, "TargetIsPresent", "TargetIsAbsent");

        targetProcessor = new VisionTargetProcessor(validator, binaryTrigger, onInteractableTargetUpdate);
    }

    public void Subscribe(IInteractionObserver observer) => onInteractableTargetUpdate += observer.OnTargetUpdate;
    public void Unsubscribe(IInteractionObserver observer) => onInteractableTargetUpdate -= observer.OnTargetUpdate;

    public override void Connect()
    {
        input.Get<InteractionInputReader>().Subscribe(this);
        //main.GetController<VisionDetector>().AddObserver(this);
    }

    public void OnTargetUpdate(VisionTarget newTarget) => targetProcessor.ProcessTarget(currentTarget, newTarget);

    public void Interact()
    {
        if (currentTarget.IsValid)
        {
            Debug.Log("INTERACT!");
        }
    }
}
