using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject potionPrefab;
    [SerializeField] private int potionsToSpawn = 5;

    [Header("Map Bounds (Adjust to match your maze size)")]
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minY = -10f;
    [SerializeField] private float maxY = 10f;

    private void Start()
    {
        SpawnPotions();
    }

    public void SpawnPotions()
    {
        if (potionPrefab == null)
        {
            Debug.LogWarning("[PotionSpawner] Potion Prefab is missing!");
            return;
        }

        for (int i = 0; i < potionsToSpawn; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY)
            );

            Instantiate(potionPrefab, randomPosition, Quaternion.identity);
        }
    }
}