using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(CollapseController))]
public class CollapseControllerEditor : Editor
{
    private CollapseController collapseController;

    private void OnEnable()
    {
        collapseController = serializedObject.targetObject as CollapseController;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(15);

        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Pull Down", GUILayout.Width(150))){
                collapseController.PullDown(Vector3.forward);
            }
        }
    }
}
#endif