using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class PotionCombination
{
    public GameObject ingredient1Prefab;
    public GameObject ingredient2Prefab;
    public GameObject bottlePrefab;
    public GameObject resultPotionPrefab;
}

public class PotionCraftManager : MonoBehaviour
{
    [Header("Potion combinations ->")]
    public List<PotionCombination> potionCombinations = new List<PotionCombination>();

    [Header("Bad Potion Prefab :(")]
    public GameObject badPotionPrefab;

    [SerializeField] private List<GameObject> currentIngredients = new List<GameObject>();
    [SerializeField] private GameObject currentBottle;

    public Transform spawnPotionPosition;

    private void Update()
    {
        if (currentIngredients.Count == 2 && currentBottle != null)
        {
            CraftPotionFromIngredients();
            ResetIngredientsAndBottle();
        }
    }

    public void AddIngredientOrBottle(GameObject obj)
    {
        if (obj.CompareTag("Ingredient") || obj.CompareTag("Bottle"))
        {
            if (obj.CompareTag("Bottle"))
            {
                currentBottle = obj;
            }
            else
            {
                currentIngredients.Add(obj);
            }
        }
        else
        {
            if (currentIngredients.Count < 2)
            {
                currentIngredients.Add(obj);
            }
            else if (currentBottle == null)
            {
                currentBottle = obj;
            }
        }

        obj.SetActive(false);
    }

    private void CraftPotionFromIngredients()
    {
        PotionCombination matchingCombination = FindMatchingCombination();
        if (matchingCombination != null)
        {
            GameObject craftedPotion = Instantiate(matchingCombination.resultPotionPrefab, spawnPotionPosition.position, Quaternion.identity);
            Debug.Log($"Crafted potion: {craftedPotion.name}");
        }
        else
        {
            Debug.LogWarning("No matching potion combination found. Crafting bad potion.");
            Instantiate(badPotionPrefab, spawnPotionPosition.position, Quaternion.identity);
        }
    }

    private PotionCombination FindMatchingCombination()
    {
        foreach (var combination in potionCombinations)
        {
            if (currentIngredients.Contains(combination.ingredient1Prefab) && currentIngredients.Contains(combination.ingredient2Prefab) && currentBottle == combination.bottlePrefab)
            {
                return combination;
            }
        }
        return null;
    }

    private void ResetIngredientsAndBottle()
    {
        currentIngredients.Clear();
        currentBottle = null;
    }

    private void PlaySplashVFX()
    {
        Debug.Log("Splash VFX");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision with {collision.gameObject.name} detected.");

        if (collision.gameObject.CompareTag("Ingredient") || collision.gameObject.CompareTag("Bottle"))
        {
            PlaySplashVFX();
            AddIngredientOrBottle(collision.gameObject);
        }
    }
}