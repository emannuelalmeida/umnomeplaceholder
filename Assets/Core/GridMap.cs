﻿using Assets.Tiles;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GridMap : ScriptableObject
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public float TotalW => tileLength * (Width - 1);

    public float TotalH => tileLength * (Height - 1);

    private Tile[,] tileMap;

    private string theme;
    private string BasePath => $"Tiles/Prefabs/{theme}/";

    private Vector3 basePosition;

    private const float tileLength = 1.6f;

    public GridMap()
    {

    }

    public void Initialize(string theme, Vector3 basePosition)
    {
        this.theme = theme;
        this.basePosition = basePosition;
    }

    public void ConstructTiles(TileType[,] tileArray)
    {
        Width = tileArray.GetLength(0);
        Height = tileArray.GetLength(1);

        tileMap = new Tile[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                InitializeTile(tileArray[x, y], x, y);
            }
        }
    }

    private void InitializeTile(TileType type, int x, int y)
    {
        string resourceName;

        switch (type)
        {
            case TileType.GROUND:
                resourceName = "ground";
                break;
            case TileType.SOLID_WALL:
                resourceName = "wall";
                break;
            case TileType.BREAK_BLOCK:
                resourceName = "box";
                break;
            case TileType.FIX_BLOCK:
                resourceName = "bridge";
                break;
            default:
                return;
        }

        var tileObject = Instantiate(Resources.Load(BasePath + resourceName),
                    basePosition + new Vector3(tileLength * x, 0, tileLength * y),
                    Quaternion.identity);

        var tile = (tileObject as GameObject).GetComponent<Tile>();

        tileMap[x, y] = tile;
    }
}
