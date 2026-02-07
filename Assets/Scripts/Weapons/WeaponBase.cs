using UnityEngine;
using System.Collections;

public class WeaponBase : MonoBehaviour
{
    [Header("Weapon Info")]
    public string weaponSlotName;
    public string weaponDisplayName;

    [Header("Weapon Stats")]
    public float fireRate; 
    public int magazineSize;
    public float damage;

    [Header("Damage Type")]
    public DamageType damageType;

    [Header("Ammo")]
    public int currentAmmo;

    [Header("Reload Settings")]
    public float reloadDuration = 2f; 
    public bool autoReload = true;

    [HideInInspector] public bool isReloading = false; 
    [HideInInspector] public float reloadElapsed = 0f; // Tracks reload progress

    private void Awake()
    {
        currentAmmo = magazineSize;
    }

    public virtual void OnEquip()
    {
        gameObject.SetActive(true);

        if (currentAmmo <= 0 && autoReload)
            StartCoroutine(ReloadCoroutine());
    }

    public virtual void OnUnequip()
    {
        gameObject.SetActive(false);
    }

    public bool TryUseAmmo()
    {
        if (isReloading)
            return false;

        if (currentAmmo > 0)
        {
            currentAmmo--;

            if (currentAmmo <= 0 && autoReload)
                StartCoroutine(ReloadCoroutine());

            return true;
        }

        if (autoReload && !isReloading)
            StartCoroutine(ReloadCoroutine());

        return false;
    }

    public void Reload()
    {
        if (!isReloading)
            StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        reloadElapsed = 0f;

        while (reloadElapsed < reloadDuration)
        {
            reloadElapsed += Time.deltaTime;
            yield return null;
        }

        currentAmmo = magazineSize;
        isReloading = false;
        reloadElapsed = 0f;
    }
}
