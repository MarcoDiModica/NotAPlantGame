using UnityEngine;
using System.Collections.Generic;

public class IngredientSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private List<GameObject> ingredientPrefabs = new List<GameObject>();
    [SerializeField] private int spawnCount = 5;
    
    [Header("Spawn Area")]
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(10f, 1f, 10f);
    
    private List<GameObject> spawnedIngredients = new List<GameObject>();
    
    private void Start()
    {
        SpawnIngredients();
    }
    
    public void SpawnIngredients()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnRandomIngredient();
        }
    }
    
    private void SpawnRandomIngredient()
    {
        if (ingredientPrefabs.Count == 0) return;
        
        GameObject prefabToSpawn = ingredientPrefabs[Random.Range(0, ingredientPrefabs.Count)];
        Vector3 spawnPosition = GetRandomSpawnPosition();
        
        GameObject spawnedIngredient = Instantiate(prefabToSpawn, spawnPosition, Random.rotation);
        spawnedIngredients.Add(spawnedIngredient);
    }
    
    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        
        return transform.position + randomOffset;
    }
    
    public void ClearAllIngredients()
    {
        foreach (GameObject ingredient in spawnedIngredients)
        {
            if (ingredient != null)
            {
                DestroyImmediate(ingredient);
            }
        }
        spawnedIngredients.Clear();
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
