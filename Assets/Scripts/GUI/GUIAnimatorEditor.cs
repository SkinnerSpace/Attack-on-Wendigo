using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GUIAnimator))]
public class GUIAnimatorEditor : Editor
{
    private GUIAnimator animator;

    private void OnEnable()
    {
        animator = serializedObject.targetObject as GUIAnimator;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}