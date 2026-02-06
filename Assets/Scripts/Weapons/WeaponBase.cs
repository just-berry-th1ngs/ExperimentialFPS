using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [Header("Weapon Info")]
    public string weaponName;

    public void OnEquip()
    {
        gameObject.SetActive(true);
    }

    public void OnUnequip()
    {
        gameObject.SetActive(false);
    }
}
