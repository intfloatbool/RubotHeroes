using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RbAddForce))]
public class RbAddForceInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        RbAddForce addForce = (RbAddForce) target;
        if (GUILayout.Button("Add force!"))
        {
            addForce.AddForce();
        }
    }
}
