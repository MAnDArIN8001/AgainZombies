using System;
using Zenject;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour
{
    public abstract event Action OnMakeShhot;
    public abstract event Action OnReload;
    public abstract event Action OnAmmoFull;
    public abstract event Action<int> OnLowAmmo;

    protected bool _isReloading;

    protected WeaponType _weaponType;

    [SerializeField] protected WeaponModel _weaponModel;

    protected MainInput _input;

    public WeaponType WeaponType => _weaponType;

    [Inject] 
    private void Initialize(MainInput input)
    {
        _input = input;
    }

    protected virtual void OnEnable()
    {
        if (_input is not null)
        {
            _input.Enable();

            _input.Gun.Reload.performed += HandleReload;
        }
    }

    protected virtual void OnDisable()
    {
        if (_input is not null)
        {
            _input.Disable();

            _input.Gun.Reload.performed += HandleReload;
        }
    }

    protected abstract void Shoot();

    protected abstract void HandleReload(InputAction.CallbackContext context);

    protected abstract void Reload();
}
