using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridSpawner))]
public class GridSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridSpawner spawner = (GridSpawner)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Spawn Grid"))
        {
            spawner.SpawnGrid();
        }

        if (GUILayout.Button("Clear Grid"))
        {
            spawner.ClearGrid();
        }
    }
}