using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;

    public static GridManager Instance;

    public Tile edgePrefab, groundPrefab;

    public Transform mainCamera;

    public Dictionary<Vector2 , Tile> tiles;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                
                Tile prefab = x == 0 || x == width - 1 || y == 0 || y == height - 1 ? edgePrefab : groundPrefab;

                var spawnedTile = Instantiate(prefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x}, {y}";

                spawnedTile.init((x + y ) % 2 == 0);

                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        mainCamera.position = new Vector3((float)(width / 2 - 0.5), (float)(height / 2 - 0.5), -10);

        GameManager.Instance.ChangeState(GameState.SPAWNPLAYER);
    }

    public int GetWidth() { return width; }

    public int GetHeight() { return height; }

    public Tile GetEntitySpawnTile()
    {
        return tiles.Where(tiles => tiles.Key.x < width && tiles.Value.walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEntitySpawnTileNoEdge()
    {
        return tiles.Where(tiles => tiles.Key.x < width - 2 && tiles.Key.x > 1 && tiles.Key.y < height - 2 && tiles.Key.y > 1 && tiles.Value.walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        tiles.TryGetValue(position, out Tile tile);
        return tile;
    }
}
