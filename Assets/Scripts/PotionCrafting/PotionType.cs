using UnityEngine;

[System.Serializable]
public class PotionType : MonoBehaviour
{
    [Header("Potion Information")]
    public string potionName;
    public int potionID;
    
    private void Awake()
    {
        if (potionID == 0)
        {
            Debug.LogWarning($"PotionType on {gameObject.name} has ID 0. Please assign a unique ID.");
        }
        
        if (string.IsNullOrEmpty(potionName))
        {
            Debug.LogWarning($"PotionType on {gameObject.name} has no name assigned.");
        }
    }

    private void Start()
    {
        if (!gameObject.CompareTag("Potion"))
        {
            Debug.LogWarning($"GameObject {gameObject.name} with PotionType should have 'Potion' tag.");
        }
    }

    public string GetPotionInfo()
    {
        return $"{potionName}";
    }

    public bool IsSameType(PotionType other)
    {
        return other != null && other.potionID == this.potionID;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (potionID < 0)
        {
            potionID = 0;
            Debug.LogWarning("Potion ID cannot be negative. Set to 0.");
        }
    }
#endif
}
