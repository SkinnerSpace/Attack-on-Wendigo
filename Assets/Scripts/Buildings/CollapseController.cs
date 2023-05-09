using System;
using System.Collections;
using UnityEngine;

public class CollapseController : MonoBehaviour, ICollapsible, ISwitchable
{
    [SerializeField] private CollapseAcceptor acceptor;
    [SerializeField] private ParticleSystem dustVFX;
    private BoxCollider collapseCollider;

    [SerializeField] private float depthMultiplier = 1f;
    [SerializeField] private float pushMultiplier = 15f;
    [SerializeField] private float angleMultiplier = 1f;

    private PropShaker shaker;
    private PropDropper dropper;
    private CollapseEstimations estimations;

    private bool started;
    private float currentTime;
    private float completeness;

    public Vector3 Position => transform.position;

    private bool isActive = true;

    public event Action<Vector3> onPushDirectionIsSet;
    private event Action<float> onUpdate;
    private event Action onCollapse;

    private void Awake()
    {
        SetupComponents();
        SubscribeComponents();
    }

    private void SetupComponents()
    {
        collapseCollider = GetComponent<BoxCollider>();

        estimations = CollapseEstimator.EstimateFor(acceptor.height);
        dropper = new PropDropper(acceptor, depthMultiplier, pushMultiplier, angleMultiplier);
        shaker = new PropShaker(estimations.frequency);
    }

    private void SubscribeComponents()
    {
        SubscribeOnUpdate(dropper);
        SubscribeOnUpdate(shaker);
    }

    public void SubscribeOnUpdate(ICollapseObserver observer) => onUpdate += observer.ReceiveCollapseUpdate;
    public void UnsubscribeFromUpdate(ICollapseObserver observer) => onUpdate -= observer.ReceiveCollapseUpdate;
    public void SubscribeOnCollapse(Action onCollapse) => this.onCollapse += onCollapse;

    public void PullDown(Vector3 pushDir)
    {
        if (isActive && !started){
            started = true;
            collapseCollider.enabled = false;
            onCollapse?.Invoke();
            onPushDirectionIsSet?.Invoke(pushDir);
            dustVFX.Play();

            dropper.Launch(pushDir);
            ShakeTheEarth();

            StartCoroutine(Collapse());
        }
    }

    IEnumerator Collapse()
    {
        while (completeness < 1f){
            KeepDestroying();
            yield return null;
        }
    }

    private void KeepDestroying()
    {
        currentTime += OldChronos.DeltaTime;
        completeness = currentTime / estimations.time;
        onUpdate?.Invoke(completeness);

        acceptor.Add(dropper.posDisplacement).Add(dropper.rotDisplacement).Add(shaker.GetPosDisplacement()).Apply();
    }

    private void ShakeTheEarth()
    {
        ShakeBuilder.Create().
                withTime(estimations.time).
                WithAxis(1f, 1f, 0f).
                WithStrength(0.1f, 1.5f).
                WithCurve(estimations.frequency, 0.3f, 0.3f).
                WithAttenuation(transform.position, GameManager.Instance.Character, 100f).
                BuildAndLaunch(ShakeManagerComponent.Instance);
    }

    public void SwitchOn(){
        isActive = true;
    }

    public void SwitchOff(){
        isActive = false;
    }
}
