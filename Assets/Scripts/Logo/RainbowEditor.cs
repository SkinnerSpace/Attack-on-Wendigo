using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RainbowController))]
public class RainbowEditor : Editor
{
    private RainbowController rainbowController;

    private void OnEnable()
    {
        rainbowController = serializedObject.targetObject as RainbowController;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        using (new EditorGUILayout.VerticalScope())
        {
            GUILayout.Space(10);

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Play"))
                {
                    rainbowController.Play();
                }

                if (GUILayout.Button("Stop"))
                {
                    rainbowController.Stop();
                }
            }
        }
    }
}