using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    public Tilemap tilemapA;
    public Tilemap tilemapB;
    public TileBase tileA; 
    public TileBase tileB; 

    [SerializeField] public int width = 60;
    [SerializeField] public int height = 60;

    [ContextMenu("Generate Tiles")]
    public void GenerateTiles()
    {
        tilemapA.ClearAllTiles();
        tilemapB.ClearAllTiles();

        int startX = -width / 2;
        int startY = -height / 2;

        for (int x = startX; x < startX + width; x++)
        {
            for (int y = startY; y < startY + height; y++)
            {
                if ((x + y) % 2 == 0)
                {
                    tilemapA.SetTile(new Vector3Int(x, y, 0), tileA);
                    tilemapB.SetTile(new Vector3Int(x, y, 0), tileB);
                }
                else
                {
                    tilemapA.SetTile(new Vector3Int(x, y, 0), tileB);
                    tilemapB.SetTile(new Vector3Int(x, y, 0), tileA);
                }
            }
        }
    }
}
