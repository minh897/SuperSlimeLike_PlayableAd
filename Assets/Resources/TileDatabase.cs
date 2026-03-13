using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileDatabase", menuName = "Tiles/Tile Database")]
public class TileDatabase : ScriptableObject
{
    public List<GameObject> tiles;
}