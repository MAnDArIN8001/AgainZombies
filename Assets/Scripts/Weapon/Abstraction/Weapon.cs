using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public virtual event Action OnReload;
    public virtual event Action OnAmmoFull;
    public virtual event Action OnMakeShhot;
    public virtual event Action<int> OnLowAmmo;
    public virtual event Action<bool> OnAimingStateChanged;
         
    protected bool _isReloading;
    protected bool _isAiming;

    protected int _currentAmmoCount;

    protected WeaponType _weaponType;

    [SerializeField] protected WeaponModel _weaponModel;

    public WeaponType WeaponType => _weaponType;

    public abstract void Shoot();

    public abstract void Reload();

    public abstract void HandleAiming(bool aimingStat);

    private void OnDestroy()
    {
        OnReload = null;
        OnLowAmmo = null;
        OnAmmoFull = null;
        OnMakeShhot = null;
        OnAimingStateChanged = null;
    }
}
