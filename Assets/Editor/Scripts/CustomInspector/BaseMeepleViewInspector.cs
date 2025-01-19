using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseMeepleView))]
public class BaseMeepleViewInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BaseMeepleView baseMeepleView = (BaseMeepleView)target;
        // if (GUILayout.Button("UpdateMeepleByType"))
        // {
        //     baseMeepleView.UpdateMeepleByMeepleType();
        // }
    }
}