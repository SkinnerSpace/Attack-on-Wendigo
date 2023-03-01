using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Wendigo))]
public class WendigoEditor : Editor
{
    private Wendigo wendigo;

    private void OnEnable()
    {
        wendigo = serializedObject.targetObject as Wendigo;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(25);

        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Activate", GUILayout.Width(100)))
            {
                wendigo.Data.IsActive = true;
                wendigo.Data.IsArrived = true;
                wendigo.SetTarget(wendigo.PendingTarget);
            }

            /*if (GUILayout.Button("Set Target", GUILayout.Width(100)))
            {
                wendigo.SetTarget(wendigo.PendingTarget);
            }*/
        }
    }
}