using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotbarSlotUI : MonoBehaviour
{
    public Image icon;
    public Image selectedBorder;
    public TextMeshProUGUI slotNumber;

    public void SetSlot(WeaponBase weapon, int index)
    {
        if (slotNumber != null)
            slotNumber.text = (index + 1).ToString();

        if (icon != null)
        {
            if (weapon != null && weapon.weaponIcon != null)
            {
                icon.sprite = weapon.weaponIcon;
                icon.color = Color.white; // ensure fully visible
            }
            else
            {
                icon.sprite = null;
                icon.color = new Color(1,1,1,0); // invisible but still active
            }

            icon.enabled = true; // always active for layout
        }

        SetSelected(false);
    }


    public void SetSelected(bool value)
    {
        if (selectedBorder != null)
            selectedBorder.gameObject.SetActive(value);
    }
}
