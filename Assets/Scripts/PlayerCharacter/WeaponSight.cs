using UnityEngine;

public class WeaponSight : MonoBehaviour
{
    private const float MAX_SCATTER = 0.1f;

    [SerializeField] private Camera cam;
    Ray ray;

    private Vector3 originalStart;
    private Vector3 originalEnd;

    private Vector3 scatteredStart;
    private Vector3 scatteredEnd;

    private void Update()
    {
        Debug.DrawLine(originalStart, originalEnd, Color.blue);
        Debug.DrawLine(scatteredStart, scatteredEnd, Color.red);
    }

    public void GetTheSpot(float precision)
    {

    }

    public void AimOffhand(float precision)
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit spot, Mathf.Infinity, ComplexLayers.Vision)){
            originalStart = ray.origin;
            originalEnd = spot.point;
        }

        Vector3 scatter = new Vector3(x: Rand.Range(-1f, 1f), y: Rand.Range(-1f, 1f), z: Rand.Range(-1f, 1f)).normalized;
        scatter *= MAX_SCATTER;

        Debug.Log("Original " + ray.direction);
        Debug.Log("Scatter " + scatter);
        ray.direction = (ray.direction + scatter).normalized;
        Debug.Log("Fucked Up " + ray.direction);

        scatteredStart = ray.origin;

        if (Physics.Raycast(ray, out RaycastHit spot2, Mathf.Infinity, ComplexLayers.Vision)){
            scatteredStart = ray.origin;
            scatteredEnd = spot2.point;
        }
    }
}
