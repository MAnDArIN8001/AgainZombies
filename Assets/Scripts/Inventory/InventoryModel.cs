using UnityEngine;

public abstract class InventoryModel : ScriptableObject
{
    [SerializeField] protected SerializableDictinary<WeaponType, GameObject> _weapons;

    public virtual GameObject GetWeaponByType(WeaponType weaponType) 
    {
        if (!_weapons.ContainsKey(weaponType))
        {
            Debug.LogError($"weapons dictionary doesnt contains weapon type: {weaponType.ToString()}");
        }

        return _weapons.GetValue(weaponType);
    }
}
