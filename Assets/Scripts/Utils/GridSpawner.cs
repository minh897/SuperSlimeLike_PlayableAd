using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToSpawn;

    [SerializeField] private int columns = 5;
    [SerializeField] private float spacingX = 2f;
    [SerializeField] private float spacingZ = 2f;

    public void SpawnGrid()
    {
        ClearGrid();
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            int row = i / columns;
            int column = i % columns;
            Vector3 spawnPosition = new Vector3(column * spacingX, 0, -row * spacingZ);
            var newObj = Instantiate(objectsToSpawn[i], spawnPosition, Quaternion.identity, transform);
            newObj.AddComponent<BoxCollider>();
            newObj.AddComponent<TileSlot>();
        }
    }

    public void ClearGrid()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
