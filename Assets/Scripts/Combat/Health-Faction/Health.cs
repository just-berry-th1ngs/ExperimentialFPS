using UnityEngine;
using System.Collections.Generic;

public class Health : MonoBehaviour, IDamageable
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Damage Modifiers")]
    public List<DamageModifier> damageModifiers = new();

    public bool IsDead => currentHealth <= 0f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount, DamageType damageType)
    {
        if (IsDead) return;

        float finalDamage = ApplyModifiers(amount, damageType);
        currentHealth -= finalDamage;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    float ApplyModifiers(float baseDamage, DamageType type)
    {
        foreach (var mod in damageModifiers)
        {
            if (mod.damageType == type)
                return baseDamage * mod.multiplier;
        }

        return baseDamage; // no modifier = normal damage
    }

    void Die()
    {
        // DO NOTHING here except notify
        SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
    }
}
