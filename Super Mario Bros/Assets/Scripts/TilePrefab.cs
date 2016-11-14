using System;
using UnityEngine;

[Serializable]
public class TilePrefab
{
    public GameObject Tile;
    public TileType TileType;

    public TilePrefab(GameObject tilePrefab, TileType tile)
    {
        Tile = tilePrefab;
        TileType = tile;
    }
}