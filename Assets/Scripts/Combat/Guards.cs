using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GuardsData", menuName = "ScriptableObjects/Guards", order = 1)]
[System.Serializable]
public class Guards : ScriptableObject
{
    public List<SwordStance> stances;
    public AtkDirection direction;
}