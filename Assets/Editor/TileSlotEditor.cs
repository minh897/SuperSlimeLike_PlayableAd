using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileSlot)), CanEditMultipleObjects]
public class TileSlotEditor : Editor
{
    private TileDatabase database;

    private void OnEnable()
    {
        database = Resources.Load<TileDatabase>("TileDatabase");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (database == null)
        {
            EditorGUILayout.HelpBox("TileDatabase not found.", MessageType.Error);
            return;
        }

        GUILayout.Space(10);
        GUILayout.Label("Tile Rotation", EditorStyles.boldLabel);
        MakeButtonRotateTile("Rotate Around Y", 1, 100);

        GUILayout.Space(10);
        GUILayout.Label("Tile Palette", EditorStyles.boldLabel);
        DrawTileGrid(70, 70);
    }

    void DrawTileGrid(int btnWidth, int btnHeight)
    {
        int columns = 5;
        int index = 0;

        while (index < database.tiles.Count)
        {
            GUILayout.BeginHorizontal();
            for (int i = 0; i < columns; i++)
            {
                if (index >= database.tiles.Count)
                    break;
                var tile = database.tiles[index];
                DrawTileButton(tile, btnWidth, btnHeight);
                index++;
            }
            GUILayout.EndHorizontal();
        }
    }

    void DrawTileButton(GameObject tile, int width, int height)
    {
        Texture preview = AssetPreview.GetAssetPreview(tile);
        if (GUILayout.Button(preview, GUILayout.Width(width), GUILayout.Height(height)))
        {
            foreach (var target in targets)
            {
                ((TileSlot)target).SwitchTile(tile);
            }
        }
    }

    private void MakeButtonRotateTile(string tileText, int direction, float buttonWidth)
    {
        if (GUILayout.Button(tileText, GUILayout.Width(buttonWidth)))
        {
            foreach (var target in targets)
            {
                ((TileSlot)target).AdjustYRotation(direction);
            }
        }
    }
}
