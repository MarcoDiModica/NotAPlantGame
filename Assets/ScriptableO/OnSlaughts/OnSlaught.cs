using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Onslaught", menuName = "ScriptableObjects/Onslaught")]
[Serializable]
public class Onslaught : ScriptableObject
{
    public MonsterAtk[] monsterAtks;
}


[Serializable]
public class MonsterAtk
{
    public AtkDirection direction;
    public float time;
}