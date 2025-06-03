using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class PotionCombination
{
    public string ingredient1Tag;
    public string ingredient2Tag;
    public string bottleTag;
    public GameObject resultPotionPrefab;
}

public class PotionCraftManager : MonoBehaviour
{
    [Header("Potion combinations ->")]
    public List<PotionCombination> potionCombinations = new List<PotionCombination>();

    [Header("Bad Potion Prefab :(")]
    public GameObject badPotionPrefab;

    [Header("Current Ingredients and Bottle")]
    [SerializeField] private List<string> currentIngredients = new List<string>();
    [SerializeField] private string currentBottle;

    [Header("Spawn Position for Crafted Potion")]
    public Transform spawnPotionPosition;

    [Header("Visual Effects")]
    public ParticleSystem splashVFX;
    public ParticleSystem goodPuffVFX;
    public ParticleSystem badPuffVFX;

    private void Update()
    {
        if (currentIngredients.Count == 2 && currentBottle.Length > 0)
        {
            CraftPotionFromIngredients();
            ResetIngredientsAndBottle();
        }
    }

    public void AddIngredientOrBottle(string tag)
    {
        if (tag == "square" || tag == "round" || tag == "vert")
        {
            if (currentBottle.Length == 0)
            {
                currentBottle = tag;
                Debug.Log("Bottle added: " + currentBottle);
            }
            else
            {
                currentIngredients.Add(tag);
            }
        }
        else
        {
            if (currentIngredients.Count >= 2 && currentBottle.Length == 0)
            {
                currentBottle = tag;
                return;
            }
            if (currentIngredients.Count >= 2)
            {
                return;
            }


            currentIngredients.Add(tag);
        }
    }

    private void CraftPotionFromIngredients()
    {
        PotionCombination matchingCombination = FindMatchingCombination();
        if (matchingCombination != null)
        {
            Instantiate(matchingCombination.resultPotionPrefab, spawnPotionPosition.position, Quaternion.identity);
            PlayPuffVFX();
        }
        else
        {
            Instantiate(badPotionPrefab, spawnPotionPosition.position, Quaternion.identity);
            PlayPuffVFX(false);
        }

    }

    private PotionCombination FindMatchingCombination()
    {
        if (currentIngredients.Count != 2 || currentBottle.Length == 0)
        {
            return null;
        }

        foreach (PotionCombination combination in potionCombinations)
        {
            if (combination.ingredient1Tag == currentIngredients[0] &&
                combination.ingredient2Tag == currentIngredients[1] &&
                combination.bottleTag == currentBottle)
            {
                return combination;
            }
            else if (combination.ingredient1Tag == currentIngredients[1] &&
                     combination.ingredient2Tag == currentIngredients[0] &&
                     combination.bottleTag == currentBottle)
            {
                return combination;
            }
        }

        return null;
    }

    private void ResetIngredientsAndBottle()
    {
        currentIngredients.Clear();
        currentBottle = string.Empty;
    }

    private void PlaySplashVFX()
    {
        if (splashVFX != null)
        {
            splashVFX.Play();
        }
        else
        {
            Debug.LogWarning("Splash VFX not assigned.");
        }
    }

    private void PlayPuffVFX(bool goodPuff = true)
    {
        ParticleSystem puffVFX = goodPuff ? goodPuffVFX : badPuffVFX;
        if (puffVFX != null)
        {
            puffVFX.Play();
        }
        else
        {
            Debug.LogWarning("Puff VFX not assigned.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TruffleEyeball") ||
            collision.gameObject.CompareTag("Artichoke") ||
            collision.gameObject.CompareTag("GreenPetalScale") ||
            collision.gameObject.CompareTag("Glistening Lead Blade") ||
            collision.gameObject.CompareTag("MushyShroom") ||
            collision.gameObject.CompareTag("DurableCap") ||
            collision.gameObject.CompareTag("StemViscera") ||
            collision.gameObject.CompareTag("vert") ||
            collision.gameObject.CompareTag("round") ||
            collision.gameObject.CompareTag("square"))
        {
            PlaySplashVFX();
            AddIngredientOrBottle(collision.gameObject.tag);
            Destroy(collision.gameObject);
        }
    }
}