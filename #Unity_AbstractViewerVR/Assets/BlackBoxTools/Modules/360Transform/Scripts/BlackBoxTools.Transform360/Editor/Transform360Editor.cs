using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BlackBoxTools.T360{
//Creates a custom Label on the inspector for all the scripts named ScriptName
// Make sure you have a ScriptName script in your
// project, else this will not work.
[CustomEditor(typeof(Transform360))]
public class Transform360Editor : Editor
{
    public override void OnInspectorGUI()
    {
        Transform360 transform360 = (Transform360) target;

        Position360 currentPosition = transform360.GetPosition360FromTransformPosition();
      //  Debug.Log(currentPosition);
        Position360 position = transform360.GetPosition360();
        if (currentPosition != position)
            transform360.SetPositionTo(currentPosition);
        EditorGUILayout.LabelField("(Below this object)");

        base.OnInspectorGUI();
    }
}}