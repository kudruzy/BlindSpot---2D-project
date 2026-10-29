using UnityEngine;
using System.Collections.Generic;


public class MazeGenerator : MonoBehaviour
{
   [Header("Prefabs")]
   [SerializeField] private GameObject wallPrefab;
   [SerializeField] private GameObject obstaclePrefab;
   [SerializeField] private GameObject exitDoorPrefab;
   [SerializeField] private GameObject enemyPrefab;


   [Header("Maze Settings")]
   [SerializeField] private int width = 31;
   [SerializeField] private int height = 31;
   [SerializeField] private float tileSize = 1.5f;
   [SerializeField] private int enemyCount = 5;


   private GameObject spawnedExitDoor;
   private List<GameObject> spawnedEnemies = new List<GameObject>();
   private List<GameObject> spawnedTiles = new List<GameObject>();


   private void Start()
   {
       GenerateMaze();
   }


   public void GenerateMaze()
   {
       Random.InitState((int)System.DateTime.Now.Ticks);


       foreach (GameObject tile in spawnedTiles)
       {
           if (tile != null) Destroy(tile);
       }
       spawnedTiles.Clear();


       foreach (GameObject enemy in spawnedEnemies)
       {
           if (enemy != null) Destroy(enemy);
       }
       spawnedEnemies.Clear();


       bool[,] mazeGrid = GenerateRandomMazeGrid(width, height);


       List<Vector2Int> openFloorTiles = new List<Vector2Int>();
       Vector2Int playerSpawn = new Vector2Int(1, 1);


       for (int x = 0; x < width; x++)
       {
           for (int y = 0; y < height; y++)
           {
               Vector3 position = new Vector3(x * tileSize, y * tileSize, 0f);
               bool isExitDoorway = (x == width - 2 && y == height - 1);


               if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
               {
                   if (!isExitDoorway && wallPrefab != null)
                   {
                       GameObject spawnedWall = Instantiate(wallPrefab, position, Quaternion.identity, transform);
                       spawnedWall.name = "WallTile";
                       spawnedTiles.Add(spawnedWall);
                   }
               }
               else if (mazeGrid[x, y])
               {
                   if (obstaclePrefab != null)
                   {
                       GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity, transform);
                       obstacle.name = "ObstacleTile";
                       spawnedTiles.Add(obstacle);
                   }
               }
               else
               {
                   int distanceFromPlayer = Mathf.Abs(x - playerSpawn.x) + Mathf.Abs(y - playerSpawn.y);
                   if (distanceFromPlayer > 3)
                   {
                       openFloorTiles.Add(new Vector2Int(x, y));
                   }
               }
           }
       }


       Vector2Int exitGridPos = new Vector2Int(width - 2, height - 1);
       SpawnExitDoor(exitGridPos);
       SpawnMultipleEnemies(openFloorTiles);
   }


   private bool[,] GenerateRandomMazeGrid(int w, int h)
   {
       bool[,] grid = new bool[w, h];


       for (int x = 0; x < w; x++)
       {
           for (int y = 0; y < h; y++)
           {
               grid[x, y] = true;
           }
       }


       Stack<Vector2Int> stack = new Stack<Vector2Int>();
       Vector2Int startPos = new Vector2Int(1, 1);
       grid[startPos.x, startPos.y] = false;
       stack.Push(startPos);


       while (stack.Count > 0)
       {
           Vector2Int current = stack.Peek();
           List<Vector2Int> neighbors = GetUnvisitedNeighbors(current, grid, w, h);


           if (neighbors.Count > 0)
           {
               Vector2Int chosen = neighbors[Random.Range(0, neighbors.Count)];
               grid[(current.x + chosen.x) / 2, (current.y + chosen.y) / 2] = false;
               grid[chosen.x, chosen.y] = false;
               stack.Push(chosen);
           }
           else
           {
               stack.Pop();
           }
       }


       return grid;
   }


   private List<Vector2Int> GetUnvisitedNeighbors(Vector2Int pos, bool[,] grid, int w, int h)
   {
       List<Vector2Int> list = new List<Vector2Int>();
       int[] dx = { 0, 0, 2, -2 };
       int[] dy = { 2, -2, 0, 0 };


       for (int i = 0; i < 4; i++)
       {
           int nx = pos.x + dx[i];
           int ny = pos.y + dy[i];


           if (nx > 0 && nx < w - 1 && ny > 0 && ny < h - 1)
           {
               if (grid[nx, ny]) list.Add(new Vector2Int(nx, ny));
           }
       }


       return list;
   }


   private void SpawnExitDoor(Vector2Int gridPos)
   {
       Vector3 worldPos = new Vector3(gridPos.x * tileSize, gridPos.y * tileSize, 0f);


       if (spawnedExitDoor == null)
       {
           if (exitDoorPrefab != null)
           {
               spawnedExitDoor = Instantiate(exitDoorPrefab, worldPos, Quaternion.identity);
           }
       }
       else
       {
           spawnedExitDoor.transform.position = worldPos;
           ExitDoor door = spawnedExitDoor.GetComponent<ExitDoor>();
           if (door != null) door.ResetDoor();
       }
   }


   private void SpawnMultipleEnemies(List<Vector2Int> availableTiles)
   {
       if (enemyPrefab == null || availableTiles.Count == 0) return;


       for (int i = 0; i < enemyCount; i++)
       {
           if (availableTiles.Count == 0) break;


           int randomIndex = Random.Range(0, availableTiles.Count);
           Vector2Int spawnGridPos = availableTiles[randomIndex];
           availableTiles.RemoveAt(randomIndex);


           Vector3 worldPos = new Vector3(spawnGridPos.x * tileSize, spawnGridPos.y * tileSize, 0f);
           GameObject enemy = Instantiate(enemyPrefab, worldPos, Quaternion.identity);
           spawnedEnemies.Add(enemy);
       }
   }
}

