using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [Header("Enemy Stats")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Damage Modifiers")]
    [Tooltip("Resistance / weakness per damage type")]
    public DamageModifier[] damageModifiers;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage, DamageType damageType)
    {
        float finalDamage = CalculateDamage(damage, damageType);
        currentHealth -= finalDamage;

        OnDamageTaken(finalDamage);

        if (currentHealth <= 0)
            Die();
    }

    protected float CalculateDamage(float baseDamage, DamageType type)
    {
        float multiplier = 1f;

        foreach (DamageModifier modifier in damageModifiers)
        {
            if (modifier.damageType == type)
            {
                multiplier = modifier.multiplier;
                break;
            }
        }

        return baseDamage * multiplier;
    }

    protected virtual void OnDamageTaken(float damage)
    {
        // Optional: play hit reaction, flash, sound, etc.
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
