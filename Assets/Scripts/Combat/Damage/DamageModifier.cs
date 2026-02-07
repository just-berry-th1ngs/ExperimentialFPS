using System;
using UnityEngine;

[Serializable]
public struct DamageModifier
{
    public DamageType damageType;

    [Tooltip("0.5 = 50% damage | 1 = normal | 2 = double damage")]
    public float multiplier;
}
