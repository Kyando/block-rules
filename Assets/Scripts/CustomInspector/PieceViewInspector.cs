using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PieceView))]
public class PieceViewInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PieceView pieceView = (PieceView)target;
        if (GUILayout.Button("UpdateKingdomColors"))
        {
            pieceView.UpdateColorsBasedOnKingdomType();
        }

        if (GUILayout.Button("RoatePieceClockwise"))
        {
            pieceView.RotatePieceClowckwise();
        }
    }
}