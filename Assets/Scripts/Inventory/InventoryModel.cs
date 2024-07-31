using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class InventoryModel : ScriptableObject
{
    protected Dictionary<WeaponType, GameObject> _weapons = new Dictionary<WeaponType, GameObject>();

    public virtual GameObject GetWeaponByType(WeaponType weaponType) 
    {
        if (!_weapons.Keys.Contains(weaponType))
        {
            Debug.LogError($"weapons dictionary doesnt contains weapon type: {weaponType.ToString()}");
        }

        return _weapons.GetValueOrDefault(weaponType);
    }
}
