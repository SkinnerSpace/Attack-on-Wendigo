using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private State state = State.Normal;

    [NonSerialized] public CharacterController body;
    [NonSerialized] public Camera mainCamera;
    [NonSerialized] public CameraFov cameraFov;
    [NonSerialized] public CharacterData data;

    private Movement movement;
    private Hookshot hookshot;
    private CameraLook cameraLook;
 
    [SerializeField] private Controllers controllerType;
    public IController controller { get; private set; }

    private void Awake()
    {
        controller = ControllerFactory.Create(controllerType);
        data = GetComponent<CharacterData>();

        body = GetComponent<CharacterController>();
        movement = transform.Find("Movement").GetComponent<Movement>();
        hookshot = transform.Find("Hookshot").GetComponent<Hookshot>();

        mainCamera = transform.Find("Camera").GetComponent<Camera>();

        if (mainCamera != null)
        {
            cameraLook = mainCamera.GetComponent<CameraLook>();
            cameraFov = mainCamera.GetComponent<CameraFov>();
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.Normal:
                movement.Move();
                cameraLook.Look();
                hookshot.Throw();
                break;

            case State.HookshotThrown:
                movement.Move();
                cameraLook.Look();
                hookshot.FlyToTarget();
                break;

            case State.HookshotFlying:
                cameraLook.Look();
                hookshot.PullBodyToTarget();
                break;
        } 
    }

    public void SetState(State state)
    {
        this.state = state;
    }
}
