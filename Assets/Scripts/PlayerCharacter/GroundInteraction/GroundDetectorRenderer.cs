using UnityEngine;
using UnityEditor;
using Character;

public class GroundDetectorRenderer : MonoBehaviour
{
    [SerializeField] private CharacterData data;

    private void OnDrawGizmos()
    {
        if (data != null) VisualizeGroundDetector();
    }

    private void VisualizeGroundDetector()
    {
        Handles.color = Color.green;
        Handles.DrawWireCube(data.Position, new Vector3(0.5f, data.Height, 0.5f));

        Handles.color = Color.red;
        Vector3 position = GroundDetector.GetDetectionPosition(data.Position, data.GroundDetectionHeight);

        Handles.DrawWireDisc(position, Vector3.up, data.GroundDetectionRadius);
        Handles.DrawWireDisc(position, Vector3.forward, data.GroundDetectionRadius);
        Handles.DrawWireDisc(position, Vector3.right, data.GroundDetectionRadius);

        Handles.color = Color.white;
    }
}
