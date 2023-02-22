using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public enum States
    {
        Demo,
        Gameplay
    }

    [SerializeField] private Transform cameraPivot;
    [SerializeField] private States state;
    [SerializeField] private bool testMode;

    void Update() => Follow();

    public void Follow()
    {
        if (cameraPivot != null)
        {
            transform.position = cameraPivot.position;

            if (state == States.Demo) 
                transform.rotation = cameraPivot.rotation;
        }
    }

    public void SetPivot(Transform cameraPivot) => this.cameraPivot = cameraPivot;
    public void SetState(States state) => this.state = state;

    private void OnDrawGizmos()
    {
        if (testMode) Follow();
    }
}
