using UnityEngine;
using UnityEditor;

public class TileEditorWindow : EditorWindow
{
    public TileDatabase tileDatabase;  // Reference your ScriptableObject
    private GameObject currentTile;     // Selected prefab
    private Vector2 scroll;
    private float gridSize = 1f;
    private float rotationY = 0f;

    [MenuItem("Tools/Tile Editor")]
    public static void ShowWindow()
    {
        GetWindow<TileEditorWindow>("Tile Editor");
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnGUI()
    {
        GUILayout.Label("Tile Editor", EditorStyles.boldLabel);

        tileDatabase = (TileDatabase)EditorGUILayout.ObjectField("Tile Database", tileDatabase, typeof(TileDatabase), false);

        gridSize = EditorGUILayout.FloatField("Grid Size", gridSize);

        if (tileDatabase == null || tileDatabase.tiles == null) return;

        scroll = EditorGUILayout.BeginScrollView(scroll);
        DrawTileGrid(70, 70);
        EditorGUILayout.EndScrollView();

        if (currentTile != null)
        {
            GUILayout.Label("Selected Tile: " + currentTile.name);
        }
    }

    void DrawTileButton(GameObject tile, int width, int height)
    {
        Texture preview = AssetPreview.GetAssetPreview(tile);
        if (GUILayout.Button(preview, GUILayout.Width(width), GUILayout.Height(height)))
        {
            currentTile = tile;
        }
    }

    void DrawTileGrid(int btnWidth, int btnHeight)
    {
        int columns = 5;
        int index = 0;

        while (index < tileDatabase.tiles.Count)
        {
            GUILayout.BeginHorizontal();
            for (int i = 0; i < columns; i++)
            {
                if (index >= tileDatabase.tiles.Count)
                    break;
                var tile = tileDatabase.tiles[index];
                DrawTileButton(tile, btnWidth, btnHeight);
                index++;
            }
            GUILayout.EndHorizontal();
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (currentTile == null) return;

        Event e = Event.current;

        // Rotate preview with R
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.R)
        {
            rotationY += 90f;

            if (rotationY >= 360f)
                rotationY = 0f;

            sceneView.Repaint();
            e.Use();
        }

        // Cancel selection with ESC
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Escape)
        {
            currentTile = null;
            Repaint();          // update the editor window
            sceneView.Repaint();
            e.Use();
            return;
        }

        // Left-click to place tile
        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 snappedPos = new(
                    Mathf.Round(hit.point.x / gridSize) * gridSize,
                    0,
                    Mathf.Round(hit.point.z / gridSize) * gridSize
                );

                // GameObject tileGO = (GameObject)PrefabUtility.InstantiatePrefab(currentTile);
                GameObject tileGO = Instantiate(currentTile);

                tileGO.transform.position = snappedPos;
                tileGO.transform.rotation = Quaternion.Euler(0, rotationY, 0);

                Undo.RegisterCreatedObjectUndo(tileGO, "Place Tile");

                e.Use();
            }
        }

        DrawTilePreview(sceneView);
    }

    void DrawTilePreview(SceneView sceneView)
    {
        if (currentTile == null) return;

        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 snappedPos = new Vector3(
                Mathf.Round(hit.point.x / gridSize) * gridSize,
                0,
                Mathf.Round(hit.point.z / gridSize) * gridSize
            );

            MeshFilter meshFilter = currentTile.GetComponent<MeshFilter>();
            MeshRenderer meshRenderer = currentTile.GetComponent<MeshRenderer>();

            if (meshFilter == null || meshRenderer == null)
                return;

            Mesh mesh = meshFilter.sharedMesh;
            Material material = meshRenderer.sharedMaterial;

            if (mesh == null || material == null)
                return;

            material.SetPass(0);

            Matrix4x4 matrix = Matrix4x4.TRS(
                snappedPos,
                Quaternion.Euler(0, rotationY, 0),
                Vector3.one
            );

            Graphics.DrawMeshNow(mesh, matrix);
        }

        sceneView.Repaint();
    }
}