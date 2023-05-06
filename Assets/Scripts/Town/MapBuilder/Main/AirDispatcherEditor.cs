using UnityEditor;
using UnityEngine;

# if UNITY_EDITOR
[CustomEditor(typeof(AirDispatcher))]
public class AirDispatcherEditor : Editor
{
    private AirDispatcher airDispatcher;

    private void OnEnable()
    {
        airDispatcher = serializedObject.targetObject as AirDispatcher;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        using (new GUILayout.VerticalScope())
        {
            GUILayout.Space(20f);

            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Test")){
                    airDispatcher.TestEscapePositionGeneration();
                }

                if (GUILayout.Button("Stop")){
                    airDispatcher.StopTesting();
                }
            }
        }
    }
}
# endif