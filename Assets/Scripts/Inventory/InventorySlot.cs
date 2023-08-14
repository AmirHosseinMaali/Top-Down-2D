using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] WeaponInfo weaponInfo;
    public WeaponInfo GetWeaponInfo() { return weaponInfo; }
}
