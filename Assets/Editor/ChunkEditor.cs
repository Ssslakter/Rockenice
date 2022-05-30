using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChunkMesh))]
public class ChunkEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ChunkMesh chunk = (ChunkMesh)target;
        if (DrawDefaultInspector())
        {
            if (chunk.autoUpdate)
            {
                chunk.Generate();
            }
        }
        if (GUILayout.Button("Generate"))
        {
            chunk.Generate();
        }
        if (GUILayout.Button("Generate Rocks"))
        {
            chunk.GenerateRocks();
        }
    }
}
