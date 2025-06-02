using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SwordStance 
{
    public Vector3 position;
    public Vector3 rotation;
}

public enum AtkDirection
{
    Up,
    Left,
    Right,
    None,
}

