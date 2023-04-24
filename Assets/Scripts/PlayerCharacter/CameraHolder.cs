using UnityEngine;

public class CameraHolder : MonoBehaviour, ICameraHolder
{
    public enum States
    {
        Demo,
        Gameplay
    }

    [SerializeField] private Transform pivot;
    [SerializeField] private States state;
    [SerializeField] private bool testMode;

    void Update() => Follow();

    public void Follow()
    {
        if (pivot != null)
        {
            transform.position = pivot.position;

            if (state == States.Demo) 
                transform.rotation = pivot.rotation;
        }
    }

    public void SetGameMode(Transform pivot)
    {
        state = States.Gameplay;
        this.pivot = pivot;
    }

    public void SetDemoMode(Transform pivot)
    {
        state = States.Demo;
        this.pivot = pivot;
    }

    private void OnDrawGizmos()
    {
        if (testMode) Follow();
    }
}
