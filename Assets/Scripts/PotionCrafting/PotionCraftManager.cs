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

    private Transform spawnPotionPosition;
    private bool checkCombinations = false;

    private void Update()
    {
        if (checkCombinations)
        {
            checkCombinations = false;
            bool potionCrafted = false;
            foreach (var combination in potionCombinations)
            {
                if (currentIngredients.Count == 2 &&
                    currentIngredients.Contains(combination.ingredient1Prefab) &&
                    currentIngredients.Contains(combination.ingredient2Prefab) &&
                    currentBottle != null &&
                    currentBottle.CompareTag("Bottle") &&
                    currentBottle.name == combination.bottlePrefab.name)
                {
                    CraftPotion(combination.resultPotionPrefab);
                    potionCrafted = true;
                    break;
                }
            }
            if (!potionCrafted)
            {
                CraftPotion(null);
            }
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

        CheckForPotionCombination();
    }

    private void CheckForPotionCombination()
    {
        if (currentIngredients.Count < 2 || currentBottle == null)
        {
            Debug.Log("Not enough ingredients or bottle to craft a potion.");
            return;
        }
        else
        {
            Debug.Log($"Current ingredients: {currentIngredients.Count}, Current bottle: {currentBottle.name}");
        }

        checkCombinations = true;
    }

    private void CraftPotion(GameObject potionPrefab)
    {
        if (potionPrefab != null)
        {
            Instantiate(potionPrefab, spawnPotionPosition.position, Quaternion.identity);
            Debug.Log("Potion crafted successfully!");
        }
        else
        {
            Instantiate(badPotionPrefab, spawnPotionPosition.position, Quaternion.identity);
            Debug.Log("Bad potion crafted :(");
        }
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