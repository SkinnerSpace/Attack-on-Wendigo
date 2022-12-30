using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private const float MULTIPLIER = 0.01f;
    private Camera cam;
    [SerializeField] private Transform player;
    private PlayerData data;

    [SerializeField] private Transform orientation;

    private float mouseX;
    private float mouseY;

    private float xRotation;
    private float yRotation;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        data = player.GetComponent<PlayerData>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        ReadInput();
        ApplyRotation();
    }

    private void ReadInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * data.mouseSensitivity * MULTIPLIER;
        xRotation -= mouseY * data.mouseSensitivity * MULTIPLIER;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

    private void ApplyRotation()
    {
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
