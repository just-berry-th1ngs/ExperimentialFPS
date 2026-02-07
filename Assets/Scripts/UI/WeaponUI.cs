using UnityEngine;
using UnityEngine.UI;
using TMPro; // Needed for TextMeshPro

public class WeaponUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI slotText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI ammoText;
    public Image weaponIcon;

    [Header("Optional Reload Bar")]
    public Image reloadBar; // Set to Filled in Inspector

    private WeaponBase currentWeapon;

    private void Update()
    {
        if (currentWeapon == null)
            return;

        // Update ammo every frame
        if (ammoText != null)
            ammoText.text = $"{currentWeapon.currentAmmo} / {currentWeapon.magazineSize}";

        // Update reload bar smoothly
        if (reloadBar != null)
        {
            if (currentWeapon.isReloading)
                reloadBar.fillAmount = currentWeapon.reloadElapsed / currentWeapon.reloadDuration;
            else
                reloadBar.fillAmount = 0f;
        }
    }

    /// <summary>
    /// Call this whenever the player switches weapons
    /// </summary>
    public void UpdateWeaponUI(WeaponBase weapon)
    {
        currentWeapon = weapon;

        if (weapon == null) return;

        if (slotText != null)
            slotText.text = weapon.weaponSlotName;

        if (nameText != null)
            nameText.text = weapon.weaponDisplayName;

        if (ammoText != null)
            ammoText.text = $"{weapon.currentAmmo} / {weapon.magazineSize}";

        if (weaponIcon != null)
        {
            if (weapon.TryGetComponent(out SpriteRenderer sr) && sr.sprite != null)
                weaponIcon.sprite = sr.sprite;
            else
                weaponIcon.sprite = null;
        }

        // Reset reload bar
        if (reloadBar != null)
            reloadBar.fillAmount = 0f;
    }
}
