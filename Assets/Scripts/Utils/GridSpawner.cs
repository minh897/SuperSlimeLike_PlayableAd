using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;

    public int columns = 5;
    public float spacingX = 2f;
    public float spacingZ = 2f;

    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            int row = i / columns;
            int column = i % columns;

            Vector3 spawnPosition = new Vector3(column * spacingX, 0,-row * spacingZ);
            Instantiate(objectsToSpawn[i], spawnPosition, Quaternion.identity, transform);
        }
    }
}
