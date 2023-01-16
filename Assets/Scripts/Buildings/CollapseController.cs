using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseController : MonoBehaviour
{
    [SerializeField] private CollapseAcceptor acceptor;
    [SerializeField] private MeshRenderer meshRenderer;
    private CollapseEstimator estimator;

    private PropShaker propShaker;
    private PropDropper propDropper;

    private bool collapsed;

    private void Awake()
    {
        estimator = GetComponent<CollapseEstimator>();
        estimator.UpdateEstimations();

        propDropper = GetComponent<PropDropper>();
        propDropper.PrepareFall(estimator.time, estimator.height);

        propShaker = GetComponent<PropShaker>();
        propShaker.PrepareShake(estimator.time, estimator.frequency);
    }

    public void Push(Vector3 pushDir)
    {
       // Debug.Log("Height " + propSize.y + " Time " + time + " Frequency " + frequency);

        if (!collapsed && propShaker != null)
        {
            collapsed = true;
            propDropper.SetDir(pushDir);
            propShaker.Launch();
            StartCoroutine(Collapse());
        }
    }

    IEnumerator Collapse()
    {
        while (!propDropper.IsDone() && !propShaker.IsDone())
        {
            propDropper.UpdateFall();
            propShaker.UpdateShake();

            acceptor.Add(propDropper.GetFallDisplacement()).Add(propShaker.GetPosDisplacement()).Apply();

            yield return null;
        }

        meshRenderer.enabled = false;
    }
}

