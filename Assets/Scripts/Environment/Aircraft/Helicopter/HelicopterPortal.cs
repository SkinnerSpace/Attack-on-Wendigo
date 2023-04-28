using UnityEngine;

public class HelicopterPortal : MonoBehaviour, ISwitchable
{
    [Header("Required Components")]
    [SerializeField] private Helicopter helicopter;
    [SerializeField] private Transform passanger;
    
    [Header("Visual")]
    [SerializeField] private float lightHeight = 100f;
    [SerializeField] private float riseTime = 0.5f;

    [Header("Collider")]
    [SerializeField] private float radius = 7.5f;
    [SerializeField] private float height = 9f;
    [SerializeField] private bool visualize;

    private MeshRenderer lightRenderer;

    private bool isActive;
    private float time;
    private float lightHeightLerp;

    private Vector3 targetScale;

    private void Awake()
    {
        helicopter.onLanded += SwitchOn;
        helicopter.onTakeOff += SwitchOff;

        lightRenderer = GetComponent<MeshRenderer>();
        targetScale = new Vector3(1f, lightHeight, 1f);
    }

    private void Update()
    {
        if (isActive)
        {
            if (time < riseTime)
            {
                time += Time.deltaTime;
                lightHeightLerp = Mathf.InverseLerp(0f, riseTime, time);
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, lightHeightLerp); // REFACTOR
            }

            transform.position = helicopter.transform.position.FlatV3();

            if (InTheArea())
            {
                Debug.Log("Entered");
            }
        }
    }

    private bool InTheArea() => WithinTheRadius() && WithinTheHeight();

    private bool WithinTheRadius()
    {
        float distance = Vector2.Distance(passanger.position.FlatV2(), transform.position);
        Debug.Log("Distance " + distance);
        return distance <= radius;
    }

    private bool WithinTheHeight()
    {
        float heightDifference = passanger.position.y - transform.position.y;
        Debug.Log("Height " + heightDifference);
        return heightDifference > 0f && heightDifference <= height;
    }

    public void SwitchOn()
    {
        isActive = true;
        lightRenderer.enabled = true;
    }

    public void SwitchOff() => isActive = false;

# if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (visualize)
        {
            float halfHeight = height / 2f;
            Vector3 cubePosition = new Vector3(transform.position.x, transform.position.y + halfHeight, transform.position.z);

            float cubeSide = radius * 2f;
            Vector3 cubeScale = new Vector3(cubeSide, height, cubeSide);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
            Gizmos.DrawWireCube(cubePosition, cubeScale);
            Gizmos.color = Color.white;
        }
    }
# endif
}