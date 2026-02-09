using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(FactionMember))]
public class EnemyBase : MonoBehaviour
{
    protected Health health;

    protected virtual void Awake()
    {
        health = GetComponent<Health>();
    }

    // Called automatically by Health via SendMessage
    protected virtual void OnDeath()
    {
        HandleDeath();
    }

    protected virtual void HandleDeath()
    {
        Destroy(gameObject);
    }
}
