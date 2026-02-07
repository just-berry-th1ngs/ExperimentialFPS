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

    [Header("Raycast")]
    public float range = 100f;
    public LayerMask hitMask;

    private WeaponBase weapon;
    private float nextFireTime;

    private void Awake()
    {
        weapon = GetComponent<WeaponBase>();
    }

    public void Fire()
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (Time.time < nextFireTime)
            return;

        if (!weapon.TryUseAmmo())
            return;

        nextFireTime = Time.time + (1f / weapon.fireRate);

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;
        Vector3 hitPoint;

        if (Physics.Raycast(ray, out hit, range, hitMask))
        {
            hitPoint = hit.point;

            if (hit.collider.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(weapon.damage, weapon.damageType);
        }
        else
        {
            hitPoint = ray.origin + ray.direction * range;
        }

        SpawnTracer(hitPoint);
    }

    void SpawnTracer(Vector3 hitPoint)
    {
        LineRenderer tracer = Instantiate(
            tracerPrefab,
            firePoint.position,
            Quaternion.identity
        );

        tracer.SetPosition(0, firePoint.position);
        tracer.SetPosition(1, hitPoint);

        Destroy(tracer.gameObject, tracerDuration);
    }
}
