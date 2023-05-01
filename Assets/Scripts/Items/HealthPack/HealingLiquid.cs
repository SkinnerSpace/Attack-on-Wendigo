using System;
using UnityEngine;

public class HealingLiquid : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private float fillAmount;
    private float time;
    private float lerp;
    private bool isBeingInjected;

    private float injectionTime;
    private Action onInjected;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void Refill()
    {
        time = 0f;
        lerp = 0f;
        fillAmount = 1f;
        isBeingInjected = false;

        UpdateFillAmount();
    }

    public void Inject(float injectionTime)
    {
        this.injectionTime = injectionTime;
        //this.onInjected = onInjected;

        isBeingInjected = true;
    }

    private void Update()
    {
        if (isBeingInjected){
            time += Time.deltaTime;
            UpdateFillAmount();

            if (time >= injectionTime)
            {
                isBeingInjected = false;
                meshRenderer.enabled = false;
                //onInjected();
            }
        }
    }

    private void UpdateFillAmount()
    {
        lerp = Mathf.InverseLerp(0f, injectionTime, time);
        lerp = Easing.QuadEaseOut(lerp);
        fillAmount = 1f - lerp;

        transform.localScale = new Vector3(1f, 1f, fillAmount);
        meshRenderer.enabled = true;
    }
}
