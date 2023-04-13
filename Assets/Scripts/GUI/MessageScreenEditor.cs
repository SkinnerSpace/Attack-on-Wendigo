using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

# if UNITY_EDITOR
[CustomEditor(typeof(MessageScreen))]
public class MessageScreenEditor : Editor
{
    private MessageScreen messageScreen;

    private void OnEnable()
    {
        messageScreen = serializedObject.targetObject as MessageScreen;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(25);

        using (new GUILayout.HorizontalScope())
        {
            using (new GUILayout.VerticalScope())
            {
                if (GUILayout.Button("Start")) {
                    messageScreen.OnStart();
                }

                if (GUILayout.Button("Game Over")){
                    messageScreen.OnGameOver();
                }

                if (GUILayout.Button("On Victory")){
                    messageScreen.OnVictory();
                }
            }
        }
    }
}
#endif