using Assets.Core.Utils;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileSplitter))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var myScript = (TileSplitter)target;

        if (GUILayout.Button("Split Tiles")) myScript.SplitTiles();
    }
}
