using UnityEngine;

[CreateAssetMenu(fileName ="WeaponModel", menuName = "Weapons/new  WeaponModel")]
public class WeaponModel : ScriptableObject
{
    [SerializeField] private int _maxAmmoCount;
    
    [SerializeField] private float _reloadTime;

    public int MAxAmmoCount => _maxAmmoCount;

    public float ReloadTime => _reloadTime;
}
