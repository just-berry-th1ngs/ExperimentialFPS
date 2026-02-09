using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    public HotbarSlotUI[] slots;
    public WeaponManager weaponManager;

    private int lastIndex = -1;

    private void Start()
    {
        InitializeSlots();
        ForceRefresh();
    }

    void InitializeSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            WeaponBase weapon =
                (i < weaponManager.weapons.Length)
                ? weaponManager.weapons[i]
                : null;

            slots[i].SetSlot(weapon, i);
        }
    }

    public void ForceRefresh()
    {
        int current = weaponManager.CurrentIndex;

        if (current == lastIndex)
            return;

        for (int i = 0; i < slots.Length; i++)
            slots[i].SetSelected(i == current);

        lastIndex = current;
    }
}
