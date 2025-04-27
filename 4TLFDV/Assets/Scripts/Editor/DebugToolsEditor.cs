// File: Assets/Scripts/Debugging/Editor/DebugToolsEditor.cs

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugTools))]
public class DebugToolsEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DebugTools debugTools = (DebugTools)target;
        SerializedObject so = new SerializedObject(debugTools);

        // References
        GUILayout.Space(10);
        EditorGUILayout.LabelField("References", EditorStyles.boldLabel);
        SerializedProperty playerRefProp = so.FindProperty("playerReference");
        EditorGUILayout.PropertyField(playerRefProp, new GUIContent("Player Reference"));

        // Gold Settings
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Debug Settings", EditorStyles.boldLabel);
        SerializedProperty goldPerClickProp = so.FindProperty("goldPerClick");
        EditorGUILayout.PropertyField(goldPerClickProp, new GUIContent("Gold Per Click"));

        // Live Player Info
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Live Player Info", EditorStyles.boldLabel);
        GUI.enabled = false;
        EditorGUILayout.IntField("Current Gold", debugTools.CurrentGold);
        EditorGUILayout.EnumPopup("Current Hand State", debugTools.CurrentHandState);
        GUI.enabled = true;

        // Debug Button
        GUILayout.Space(10);
        if (GUILayout.Button("Add Debug Gold"))
        {
            debugTools.ManualAddGold();
        }

        so.ApplyModifiedProperties();
    }

}
