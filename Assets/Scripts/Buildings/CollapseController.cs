using System;
using System.Collections;
using UnityEngine;

public class CollapseController : MonoBehaviour, ICollapsible
{
    [SerializeField] private CollapseAcceptor acceptor;
    [SerializeField] private ParticleSystem dustVFX;
    private BoxCollider collapseCollider;

    [SerializeField] private float depthMultiplier = 1f;

    private PropShaker shaker;
    private PropDropper dropper;
    private CollapseEstimations estimations;

    private bool started;
    private float currentTime;
    private float completeness;

    public Vector3 Position => transform.position;

    public event Action<float> notifyOnUpdate;

    private void Awake()
    {
        SetupComponents();
        SubscribeComponents();
    }

    private void SetupComponents()
    {
        collapseCollider = GetComponent<BoxCollider>();

        estimations = CollapseEstimator.EstimateFor(acceptor.height);
        dropper = new PropDropper(acceptor, depthMultiplier);
        shaker = new PropShaker(estimations.frequency);

    }

    private void SubscribeComponents()
    {
        Subscribe(dropper);
        Subscribe(shaker);
    }

    public void Subscribe(ICollapseObserver observer) => notifyOnUpdate += observer.ReceiveCollapseUpdate;

    public void PullDown(Vector3 pushDir)
    {
        if (!started){
            started = true;
            collapseCollider.enabled = false;
            dustVFX.Play();

            dropper.Launch(pushDir);
            shaker.Launch();
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
        notifyOnUpdate(completeness);

        acceptor.Add(dropper.posDisplacement).Add(dropper.rotDisplacement).Add(shaker.GetPosDisplacement()).Apply();
    }

    private void ShakeTheEarth()
    {
        ScreenShake.Create().
                withTime(estimations.time).
                WithAxis(1f, 1f, 0f).
                WithStrength(0.1f, 1.5f).
                WithCurve(estimations.frequency, 0.3f, 0.3f).
                WithAttenuationToObj(CharacterData.Instance.transform, transform.position, 100f).Launch(ShakeManagerComponent.Instance);
    }
}