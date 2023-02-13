using UnityEngine;

public class HelicopterBlades : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    private float rotation;

    private void Update()
    {
        rotation += rotationSpeed * OldChronos.DeltaTime;
        if (rotation >= 360f) rotation = 0f;

        transform.localEulerAngles = new Vector3(0f, rotation, 0f);
    }
}
