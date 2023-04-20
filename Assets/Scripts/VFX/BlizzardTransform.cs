using UnityEngine;

public class BlizzardTransform : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float size = 1f;
    [SerializeField] private float changeSpeed = 0.5f;

    [SerializeField] private bool permanent;
    [SerializeField] private AnimationCurve sizeProgression;

    private float targetSize = 1f;

    public float Size => size;

    private void Start(){
        GameEvents.current.onDeathProgressUpdate += SetTargetSize;
    }

    private void Update()
    {
        size = Mathf.Lerp(size, targetSize, changeSpeed * Time.deltaTime);

        float height = ((1f - size) * 10f) + 1f;
        transform.localScale = new Vector3(size, height, size);
    }

    private void SetTargetSize(float progress)
    {
        targetSize = sizeProgression.Evaluate(progress);
    }
}
