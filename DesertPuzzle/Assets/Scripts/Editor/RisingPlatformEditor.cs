using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RisingPlatforms))]
[CanEditMultipleObjects]
public class RisingPlatformEditor : Editor
{
    SerializedProperty startPos;
    SerializedProperty endPos;    

    void OnEnable()
    {
        startPos = serializedObject.FindProperty("startingPosition");
        endPos = serializedObject.FindProperty("endPosition");       
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        RisingPlatforms script = (RisingPlatforms)target;

        if (GUILayout.Button("Set Start Position"))
        {
            startPos.vector3Value = script.Platform.position;
        }
        if (GUILayout.Button("Set End Position"))
        {
            endPos.vector3Value = script.Platform.position;
        }
        if (GUILayout.Button("Set To Start Position"))
        {
            script.Platform.position = startPos.vector3Value;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
