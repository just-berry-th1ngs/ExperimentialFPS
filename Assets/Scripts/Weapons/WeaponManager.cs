using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapons In Hand")]
    public WeaponBase[] weapons;

    private int currentIndex = 0;
    private WeaponBase currentWeapon;

    private void Start()
    {
        InitializeWeapons();

        if (weapons.Length > 0)
            EquipWeapon(0);
    }

    private void Update()
    {
        HandleScrollInput();
        HandleNumberKeys();
    }

    void HandleScrollInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
            NextWeapon();
        else if (scroll < 0f)
            PreviousWeapon();
    }

    void HandleNumberKeys()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            // Alpha1 starts at index 0
            KeyCode key = KeyCode.Alpha1 + i;

            if (Input.GetKeyDown(key))
            {
                EquipWeapon(i);
                break;
            }
        }
    }

    void InitializeWeapons()
    {
        foreach (WeaponBase weapon in weapons)
        {
            if (weapon != null)
                weapon.OnUnequip();
        }
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length)
            return;

        if (weapons[index] == null)
            return;

        if (currentWeapon != null)
            currentWeapon.OnUnequip();

        currentIndex = index;
        currentWeapon = weapons[index];
        currentWeapon.OnEquip();
    }

    void NextWeapon()
    {
        int next = (currentIndex + 1) % weapons.Length;
        EquipWeapon(next);
    }

    void PreviousWeapon()
    {
        int prev = currentIndex - 1;
        if (prev < 0)
            prev = weapons.Length - 1;

        EquipWeapon(prev);
    }
}
