using Assets.Core.Utils;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteMetadataGenerator))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var myScript = (SpriteMetadataGenerator)target;

        if (GUILayout.Button("Generate metadata")) myScript.GenerateSpriteMetadata();
    }
}
