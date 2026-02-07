using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public WeaponBase weapon;           
    public TextMeshProUGUI ammoText;    
    public Image reloadBar;             

    private void Update()
    {
        if (weapon == null || ammoText == null || reloadBar == null)
            return;

        // Update ammo text
        ammoText.text = weapon.currentAmmo + " / " + weapon.magazineSize;

        // Update reload bar based on reload progress
        if (weapon.isReloading)
        {
            reloadBar.fillAmount = weapon.reloadElapsed / weapon.reloadDuration;
        }
        else
        {
            reloadBar.fillAmount = 0f;
        }
    }
}
