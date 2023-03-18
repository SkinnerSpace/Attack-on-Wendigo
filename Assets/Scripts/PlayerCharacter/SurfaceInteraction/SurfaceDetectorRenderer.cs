using UnityEngine;
using UnityEditor;
using Character;

public class SurfaceDetectorRenderer : MonoBehaviour
{
    [SerializeField] private CharacterData data;

    private void OnDrawGizmos()
    {
        if (data != null) VisualizeSurfaceDetector();
    }

    private void VisualizeSurfaceDetector()
    {
        Handles.color = Color.green;
        Handles.DrawWireCube(data.Position, new Vector3(0.5f, data.Height, 0.5f));

        Handles.color = Color.red;

        Vector3 start = SurfaceDetector.GetRayPosition(data.Position, data.Height);
        Vector3 end = start + Vector3.down;
        Handles.DrawLine(start, end);

        Handles.color = Color.white;
    }
}
