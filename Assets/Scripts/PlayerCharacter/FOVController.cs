using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FOVController : MonoBehaviour 
{
    [SerializeField] private float minFOV = 80f;
    [SerializeField] private float maxFOV = 100f;
    [SerializeField] private float defaultSmooth = 5f;
    private float currentFOV;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();

        currentFOV = minFOV;
    }

    private void Update()
    {
        UpdateFOV(PlayerHorizontalMovement.speedMagnitude);
    }

    private void UpdateFOV(float magnitude)
    {
        float targetFOV = magnitude > 0f ? maxFOV : minFOV;
        float smooth = magnitude > 0f ? magnitude : (defaultSmooth * Time.deltaTime);

        currentFOV = Mathf.Lerp(currentFOV, targetFOV, smooth);
        cam.fieldOfView = currentFOV;
    }
}
