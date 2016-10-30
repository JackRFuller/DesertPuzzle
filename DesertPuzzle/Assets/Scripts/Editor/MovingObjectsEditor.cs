using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MovingObjects))]
[CanEditMultipleObjects]
public class MovingObjectsEditor : Editor
{
    SerializedProperty startPos;
    SerializedProperty endPos;

    void OnEnable()
    {
        startPos = serializedObject.FindProperty("startPosition");
        endPos = serializedObject.FindProperty("endPosition");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        DrawDefaultInspector();

        MovingObjects script = (MovingObjects)target;

        if(GUILayout.Button("Set Start Position"))
        {
            startPos.vector3Value = script.transform.position;

        }
        if (GUILayout.Button("Set End Position"))
        {
            endPos.vector3Value = script.transform.position;
        }
        if (GUILayout.Button("Set To Start Position"))
        {
            script.transform.position = startPos.vector3Value;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
