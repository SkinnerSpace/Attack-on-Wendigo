using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GroundDetectorBehavior))]
public class GroundDetectorEditor : Editor
{
    private SerializedObject soDetector;
    private GroundDetectorBehavior detector;
    private CharacterData data;
    
    private void OnEnable()
    {
        soDetector = serializedObject;
        detector = soDetector.targetObject as GroundDetectorBehavior;
        data = detector.data;

        SceneView.duringSceneGui += DuringSceneGUI;
    }

    private void OnDisable() => SceneView.duringSceneGui -= DuringSceneGUI;

    public override void OnInspectorGUI() => base.OnInspectorGUI();

    private void DuringSceneGUI(SceneView sceneView)
    {
        Handles.color = Color.green;
        Handles.DrawWireCube(data.Position, new Vector3(0.5f, data.Height, 0.5f));

        Handles.color = Color.red;
        Vector3 position = GroundDetectionHandler.GetDetectionPosition(data.Position, data.GroundDetectionHeight);

        Handles.DrawWireDisc(position, Vector3.up, data.GroundDetectionRadius);
        Handles.DrawWireDisc(position, Vector3.forward, data.GroundDetectionRadius);
        Handles.DrawWireDisc(position, Vector3.right, data.GroundDetectionRadius);

        Handles.color = Color.white;
    }
}
