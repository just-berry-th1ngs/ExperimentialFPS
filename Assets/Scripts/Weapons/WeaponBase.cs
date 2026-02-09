using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [Header("Weapon Info")]
    public string weaponSlotName;
    public string weaponDisplayName;
    public Sprite weaponIcon;

    [Header("Weapon Stats")]
    public float fireRate;
    public int magazineSize;
    public float damage;

    [Header("Damage Type")]
    public DamageType damageType;

    [Header("Ammo")]
    public int currentAmmo;

    [Header("Reload")]
    public float reloadDuration = 1.5f;
    [HideInInspector] public bool isReloading;
    [HideInInspector] public float reloadElapsed;

    private void Awake()
    {
        currentAmmo = magazineSize;
    }

    public virtual void OnEquip()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnUnequip()
    {
        gameObject.SetActive(false);
    }

    public bool TryUseAmmo()
    {
        if (isReloading) return false;

        if (currentAmmo > 0)
        {
            currentAmmo--;
            return true;
        }

        return false;
    }

    public void Reload()
    {
        if (isReloading || currentAmmo == magazineSize)
            return;

        StartCoroutine(ReloadRoutine());
    }

    private System.Collections.IEnumerator ReloadRoutine()
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
    }
}
