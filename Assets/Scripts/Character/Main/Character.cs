using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private State state = State.Normal;

    [NonSerialized] public CharacterController body;
    [NonSerialized] public Camera mainCamera;
    [NonSerialized] public CameraFov cameraFov;
    [NonSerialized] public CharacterData data;

    [NonSerialized] public Movement movement;
    [NonSerialized] public Hookshot hookshot;
    [NonSerialized] public CameraLook cameraLook;
 
    [SerializeField] public Controllers controllerType;
    public IController controller;

    private void Awake()
    {
        GetComponent<CharacterBootstrap>().Bootstrap(this);
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
