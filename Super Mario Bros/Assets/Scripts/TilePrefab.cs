using System;
using UnityEngine;

[Serializable]
public class TilePrefab
{
    public GameObject Tile;
    public Tiles TileType;

    public TilePrefab(GameObject tilePrefab, Tiles tile)
    {
        Tile = tilePrefab;
        TileType = tile;
    }
}