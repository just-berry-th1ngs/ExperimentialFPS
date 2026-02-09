using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("UI")]
    public WeaponUI weaponUI;
    public HotbarUI hotbarUI;

    [Header("Weapons In Hand")]
    public WeaponBase[] weapons;

    public int CurrentIndex => currentIndex;

    private int currentIndex;
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
        HandleFireInput();
        HandleReloadInput();
    }

    void InitializeWeapons()
    {
        foreach (WeaponBase weapon in weapons)
            if (weapon != null)
                weapon.OnUnequip();
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

        if (weaponUI != null)
            weaponUI.UpdateWeaponUI(currentWeapon);

        if (hotbarUI != null)
            hotbarUI.ForceRefresh();
    }

    void HandleScrollInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
            EquipWeapon((currentIndex + 1) % weapons.Length);
        else if (scroll < 0f)
            EquipWeapon((currentIndex - 1 + weapons.Length) % weapons.Length);
    }

    void HandleNumberKeys()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                EquipWeapon(i);
        }
    }

    void HandleFireInput()
    {
        if (currentWeapon == null) return;

        if (Input.GetMouseButton(0))
        {
            var shooter = currentWeapon.GetComponent<WeaponRaycastShooter>();
            if (shooter != null)
                shooter.Fire();
        }
    }

    void HandleReloadInput()
    {
        if (currentWeapon == null) return;

        if (Input.GetKeyDown(KeyCode.R))
            currentWeapon.Reload();
    }
}
