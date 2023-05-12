using UnityEditor;
using UnityEngine;

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

            if (GUILayout.Button("Idle"))
            {
                controller.SetStage(LogoAnimationStages.Idle);
            }

            if (GUILayout.Button("Pierced"))
            {
                controller.SetStage(LogoAnimationStages.Pierced);
            }

            if (GUILayout.Button("Final"))
            {
                controller.SetStage(LogoAnimationStages.Final);
            }
        }
    }
}


