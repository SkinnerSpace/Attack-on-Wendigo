﻿using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    private const float VERTICAL_ADJUSTMENT = 0.3f;
    private const float LAND_ADJUSTMENT = 5f;
    private const float STABILITY_MODIFIER = 1f;

    [SerializeField] private float rotationIntensity = 3f;
    [SerializeField] private float smoothRotation = 5f;

    private VerticalTuner verticalTuner;
    private SwayController controller;
    private IAimController aimController;
    private Quaternion originalRotation;

    private Vector2 input;

    private void Awake()
    {
        controller = GetComponent<SwayController>();
        originalRotation = transform.localRotation;
    }

    private void Start()
    {
        aimController = controller.aimController;
    }

    public WeaponRotator InitializeOnTake(VerticalTuner verticalTuner)
    {
        this.verticalTuner = verticalTuner;
        return this;
    }

    public void ReadInput()
    {
        float inputY = GetVerticalInput();

        input = new Vector2(
            controller.input.x,
            inputY) * 0.5f;

        if (aimController != null){
            input *= aimController.GetStability(STABILITY_MODIFIER);
        }
    }

    private float GetVerticalInput()
    {
        float inputY = controller.input.y;
        inputY = Mathf.Clamp(inputY, 0f, 1f);
        inputY = verticalTuner.IncreaseVerticalInput(inputY, VERTICAL_ADJUSTMENT);

        return inputY;
    }

    public Quaternion OffsetRotation(Quaternion currentRotation)
    {
        Quaternion horizontalAdjustment = Quaternion.AngleAxis(-rotationIntensity * input.x, Vector3.up);
        Quaternion verticalAdjustment = Quaternion.AngleAxis(rotationIntensity * input.y, Vector3.right);

        Quaternion fullyOffsettedRotation = originalRotation * horizontalAdjustment * verticalAdjustment;
        Quaternion rotation = Quaternion.Lerp(currentRotation, fullyOffsettedRotation, smoothRotation * Time.deltaTime);

        return rotation;
    }
}