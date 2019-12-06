﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomRobotCommandRunner))]
public class RobotCommandEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CustomRobotCommandRunner runner = (CustomRobotCommandRunner) target;
        if (GUILayout.Button("Run command!"))
        {
            runner.RunCommand();
        }
    }
}