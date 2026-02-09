using UnityEngine;
using System.Collections;

public class WeaponRaycastShooter : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public Transform firePoint;

    [Header("Tracer")]
    public LineRenderer tracerPrefab;
    public float tracerDuration = 0.05f;
    public Color tracerColor = Color.white; 
    
    [Header("Raycast")]
    public float range = 100f;
    public LayerMask hitMask;

    private WeaponBase weapon;
    private FactionMember attackerFaction;
    private float nextFireTime;

    private void Awake()
    {
        weapon = GetComponent<WeaponBase>();
        attackerFaction = GetComponent<FactionMember>();
    }

    public void Fire()
    {
        if (!gameObject.activeInHierarchy) return;
        if (Time.time < nextFireTime) return;
        if (!weapon.TryUseAmmo()) return;

        nextFireTime = Time.time + (1f / weapon.fireRate);

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        Vector3 hitPoint;

        if (Physics.Raycast(ray, out hit, range, hitMask))
        {
            hitPoint = hit.point;
            HandleHit(hit, hitPoint);
        }
        else
        {
            hitPoint = ray.origin + ray.direction * range;
        }

        SpawnTracer(hitPoint);
    }

    private void HandleHit(RaycastHit hit, Vector3 hitPoint)
    {
        bool canDamage = true;

        if (hit.collider.TryGetComponent(out IDamageable damageable))
        {
            var targetFaction = hit.collider.GetComponent<FactionMember>();
            if (attackerFaction != null && targetFaction != null)
                canDamage = FactionRules.CanDamage(attackerFaction.faction, targetFaction.faction);

            if (canDamage)
            {
                damageable.TakeDamage(weapon.damage, weapon.damageType);
                Debug.Log($"Hit {hit.collider.name}, Damage: {weapon.damage}, Type: {weapon.damageType}");
            }
        }

        if (hit.collider.TryGetComponent(out EnemyHitEffect hitEffect))
        {
            hitEffect.TriggerHit(tracerColor);
        }
    }

    private void SpawnTracer(Vector3 hitPoint)
    {
        if (tracerPrefab == null) return;

        LineRenderer tracer = Instantiate(tracerPrefab, firePoint.position, Quaternion.identity);
        tracer.SetPosition(0, firePoint.position);
        tracer.SetPosition(1, hitPoint);

        tracer.startColor = tracerColor;
        tracer.endColor = tracerColor;

        Destroy(tracer.gameObject, tracerDuration);
    }
}