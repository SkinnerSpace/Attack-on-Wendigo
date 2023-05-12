using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LogoPushController))]
public class LogoPushControllerEditor : Editor
{
    private LogoPushController pushController;

    private void OnEnable()
    {
        pushController = serializedObject.targetObject as LogoPushController;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        using (new GUILayout.VerticalScope())
        {
            GUILayout.Space(10);

            if (GUILayout.Button("Play")){
                pushController.Push();
            }
        }
    }
}