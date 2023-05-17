using UnityEditor;
using UnityEngine;

# if UNITY_EDITOR
[CustomEditor(typeof(BoltsController))]
public class BoltsControllerEditor : Editor
{
    private BoltsController controller;

    private void OnEnable()
    {
        controller = serializedObject.targetObject as BoltsController;
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
#endif