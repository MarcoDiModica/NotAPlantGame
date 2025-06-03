using UnityEngine;
using System.Collections.Generic;

public class IngredientSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private List<GameObject> ingredientPrefabs = new List<GameObject>();
    
    [Header("Spawn Area")]
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(10f, 1f, 10f);
    
    [Header("Drop Names")]
    [SerializeField] private string[] dropNames;
    
    private List<GameObject> spawnedIngredients = new List<GameObject>();
    
    private void Start()
    {
        SpawnIngredientsFromDrops();
    }
    
    public void SpawnIngredientsFromDrops()
    {
        ClearAllIngredients();
        
        for (int i = 0; i < dropNames.Length && i < ingredientPrefabs.Count; i++)
        {
            int dropAmount = PlayerPrefs.GetInt(dropNames[i], 0);
            if (dropAmount < 1) dropAmount = 1;
            
            for (int j = 0; j < dropAmount; j++)
            {
                SpawnSpecificIngredient(i);
            }
            
            PlayerPrefs.SetInt(dropNames[i], 0);
        }
    }
    
    private void SpawnSpecificIngredient(int prefabIndex)
    {
        if (prefabIndex >= ingredientPrefabs.Count) return;
        
        GameObject prefabToSpawn = ingredientPrefabs[prefabIndex];
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
