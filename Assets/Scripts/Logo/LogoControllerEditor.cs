using UnityEditor;
using UnityEngine;

# if UNITY_EDITOR
[CustomEditor(typeof(LogoController))]
public class LogoControllerEditor : Editor
{
    private LogoController controller;

    private void OnEnable()
    {
        controller = serializedObject.targetObject as LogoController;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        using (new GUILayout.VerticalScope())
        {
            GUILayout.Space(10);

            if (GUILayout.Button("Play"))
            {
                controller.Play();
            }

            if (GUILayout.Button("Reset"))
            {
                controller.ResetState();
            }
        }
    }
}
# endif

