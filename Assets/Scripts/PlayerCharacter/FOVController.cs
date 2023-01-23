using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FOVController : MonoBehaviour 
{
    private const float ADDITIONAL_FOV_MULTIPLIER = 0.75f;

    [SerializeField] PlayerHorizontalMover horizontalMovement;

    [SerializeField] private float minFOV = 80f;
    [SerializeField] private float maxFOV = 100f;
    [SerializeField] private float defaultSmooth = 5f;
    
    private float additionalFOV;
    private float currentFOV;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        currentFOV = minFOV;
        additionalFOV = (maxFOV - minFOV) * ADDITIONAL_FOV_MULTIPLIER;
    }

    private void Update()
    {
        UpdateFOV(horizontalMovement.velocityFOVModifier);
    }

    private void UpdateFOV(float magnitude)
    {
        float targetFOV = minFOV + (additionalFOV * magnitude);
        targetFOV = Mathf.Min(maxFOV, targetFOV);

        float smooth = magnitude > 0f ? magnitude : (defaultSmooth * Time.deltaTime);

        currentFOV = Mathf.Lerp(currentFOV, targetFOV, smooth);

        cam.fieldOfView = currentFOV;
    }
}
