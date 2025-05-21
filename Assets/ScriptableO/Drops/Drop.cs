using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Drop", menuName = "ScriptableObjects/Drop")]
[Serializable]
public class Drop : MonoBehaviour
{
    public string name;
    public Sprite sprite;

    [Range(0f, 1f)] public float chance;
}
