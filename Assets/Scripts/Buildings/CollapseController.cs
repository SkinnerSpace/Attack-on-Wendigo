using System;
using System.Collections;
using UnityEngine;

public class CollapseController : MonoBehaviour
{
    [SerializeField] private CollapseAcceptor acceptor;
    [SerializeField] private CollapseSFXPlayer collapseSFXPlayer;
    private BoxCollider collapseCollider;

    private PropShaker shaker;
    private PropDropper dropper;

    private bool collapsed;

    private float currentTime;
    private float completeness;
    public event Action<float> notifyOnUpdate;

    private CollapseEstimations estimated;

    private void Awake()
    {
        collapseCollider = GetComponent<BoxCollider>();
        collapseCollider.enabled = true;

        PrepareToCollapse();
    }

    private void PrepareToCollapse()
    {
        estimated = CollapseEstimator.EstimateFor(acceptor.height);

        dropper = new PropDropper(acceptor);
        shaker = new PropShaker(estimated.frequency);

        notifyOnUpdate += dropper.SetDisplacement;
        notifyOnUpdate += shaker.SetCompleteness;
        if(collapseSFXPlayer != null) collapseSFXPlayer.SubscribeTo(this);
    }

    public void Subscribe()

    public void PullDown(Vector3 pushDir)
    {
        if (!collapsed)
        {
            collapsed = true;
            collapseCollider.enabled = false;

            LaunchCollapse(pushDir);
        }
    }

    private void LaunchCollapse(Vector3 pushDir)
    {
        if (collapseSFXPlayer != null) collapseSFXPlayer.PlayFallSFX();

        dropper.Launch(pushDir);
        shaker.Launch();
        StartCoroutine(Collapse());
    }

    IEnumerator Collapse()
    {
        while (completeness < 1f){
            KeepDestroying();
            yield return null;
        }

        SweepTheTrash();
    }

    private void KeepDestroying()
    {
        currentTime += Chronos.DeltaTime;
        completeness = currentTime / estimated.time;
        notifyOnUpdate(completeness);

        acceptor.Add(dropper.posDisplacement).Add(dropper.rotDisplacement).Add(shaker.GetPosDisplacement()).Apply();
    }

    private void SweepTheTrash()
    {
        Debug.Log("COLLAPSED!");
        acceptor.Disappear();
    }
}