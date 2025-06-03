using System.Collections.Generic;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "GuardsDataObject", menuName = "ScriptableObjects/GuardObject", order = 1)]
[Serializable]
public class Guard : ScriptableObject
{
    public List<SwordStance> stances;
    public AtkDirection direction;
}
