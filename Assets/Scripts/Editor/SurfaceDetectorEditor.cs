using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SurfaceDetectorBehavior))]
public class SurfaceDetectorEditor : Editor
{
    private SerializedObject soDetector;
    private SurfaceDetectorBehavior detector;
    private CharacterData data;

    private void OnEnable()
    {
        soDetector = serializedObject;
        detector = soDetector.targetObject as SurfaceDetectorBehavior;
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

        Vector3 start = SurfaceDetector.GetRayPosition(data.Position, data.Height);
        Vector3 end = start + Vector3.down;
        Handles.DrawLine(start, end);

        Handles.color = Color.white;
    }
}
