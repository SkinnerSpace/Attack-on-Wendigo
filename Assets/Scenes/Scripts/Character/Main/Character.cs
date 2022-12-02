using UnityEngine;

public class Character : MonoBehaviour
{
    private State state = State.Normal;

    [SerializeField] private Movement movement;
    [SerializeField] private CameraLook cameraLook;
    [SerializeField] private Hookshot hookshot;

    private void Update()
    {
        switch (state)
        {
            case State.Normal:
                movement.Process();
                cameraLook.Look();
                hookshot.TryThrow();
                break;

            case State.HookshotThrown:
                movement.Process();
                cameraLook.Look();
                hookshot.TryThrow();
                break;

            case State.HookshotFlying:
                hookshot.Movement();
                cameraLook.Look();
                break;
        } 
    }

    public void SetState(State state)
    {
        this.state = state;
    }
}
