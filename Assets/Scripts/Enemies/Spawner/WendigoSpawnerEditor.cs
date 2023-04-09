using UnityEditor;
using UnityEngine;

# if UNITY_EDITOR
[CustomEditor(typeof(WendigoSpawner))]
public class WendigoSpawnerEditor : Editor
{
    private WendigoSpawner spawner;

    private void OnEnable()
    {
        spawner = serializedObject.targetObject as WendigoSpawner;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        using (new GUILayout.VerticalScope())
        {
            GUILayout.Space(10);

            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Spawn"))
                {
                    spawner.Spawn();
                }
            }
        }
    }
}
# endif
