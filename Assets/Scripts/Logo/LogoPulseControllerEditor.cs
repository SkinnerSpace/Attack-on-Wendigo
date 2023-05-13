using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LogoPulseController))]
public class LogoPulseControllerEditor : Editor
{
    private LogoPulseController controller;

    private void OnEnable()
    {
        controller = serializedObject.targetObject as LogoPulseController;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        using (new GUILayout.VerticalScope())
        {
            GUILayout.Space(20);

            if (GUILayout.Button("Play"))
            {
                controller.Play();
            }

            if (GUILayout.Button("Stop"))
            {
                controller.Stop();
            }
        }
    }
}