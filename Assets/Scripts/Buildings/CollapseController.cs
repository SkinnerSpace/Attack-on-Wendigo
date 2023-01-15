using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseController : MonoBehaviour
{
    [SerializeField] private CollapseAcceptor acceptor;
    [SerializeField] private float time;

    private PropShaker propShaker;
    private PropDropper propDropper;

    private bool collapsed;

    private void Awake()
    {
        propDropper = GetComponent<PropDropper>();
        propDropper.PrepareFall(time);

        propShaker = GetComponent<PropShaker>();
        propShaker.PrepareShake(time);
    }

    public void Launch()
    {
        if (!collapsed && propShaker != null)
        {
            collapsed = true;
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
    }
}
