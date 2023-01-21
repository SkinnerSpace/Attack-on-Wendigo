using System.Collections;
using UnityEngine;

public class CollapseController : MonoBehaviour
{
    [SerializeField] private CollapseAcceptor acceptor;
    [SerializeField] private CollapseSFXPlayer collapseSFXPlayer;
    
    private BoxCollider collapseCollider;
    private CollapseEstimator estimator;
    private PropShaker shaker;
    private PropDropper dropper;

    private bool collapsed;

    private void Awake()
    {
        InitializeComponents();
        PrepareToCollapse();
    }

    private void InitializeComponents()
    {
        collapseCollider = GetComponent<BoxCollider>();

        estimator = new CollapseEstimator();
        dropper = new PropDropper();
        shaker = new PropShaker(); 
    }

    private void PrepareToCollapse()
    {
        estimator.EstiamteFor(acceptor);
        collapseCollider.size = estimator.propSize;

        dropper.PrepareToFall(estimator, acceptor);
        shaker.PrepareToShake(estimator);
    }

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
        while (IsNotDestroyedYet()){
            KeepDestroying();
            yield return null;
        }

        SweepTheTrash();
    }

    private bool IsNotDestroyedYet() => !dropper.IsDone() && !shaker.IsDone();

    private void KeepDestroying()
    {
        dropper.UpdateFall();
        shaker.UpdateShake();

        if (dropper.Completeness >= 0.6f)
            StopCollapseSFX();

        acceptor.Add(dropper.posDisplacement).Add(dropper.rotDisplacement).Add(shaker.GetPosDisplacement()).Apply();
    }


    private bool collapseSFXHasStopped;
    private void StopCollapseSFX()
    {
        if (!collapseSFXHasStopped){
            Debug.Log("STOP EVEMT");
            collapseSFXHasStopped = true;
            if (collapseSFXPlayer != null) collapseSFXPlayer.Stop();
        }
    }

    private void SweepTheTrash()
    {
        acceptor.Disappear();
    }
}
