using System.Collections;
using UnityEngine;

public class CollapseController : MonoBehaviour
{
    [SerializeField] private CollapseAcceptor acceptor;
    
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

    public void Push(Vector3 pushDir)
    {
        if (!collapsed)
        {
            collapsed = true;
            collapseCollider.enabled = false;

            dropper.Launch(pushDir);
            shaker.Launch();
            StartCoroutine(Collapse());
        }
    }

    IEnumerator Collapse()
    {
        while (!dropper.IsDone() && !shaker.IsDone())
        {
            dropper.UpdateFall();
            shaker.UpdateShake();

            acceptor.Add(dropper.posDisplacement).Add(dropper.rotDisplacement).Add(shaker.GetPosDisplacement()).Apply();

            yield return null;
        }

        acceptor.Disappear();
    }
}

