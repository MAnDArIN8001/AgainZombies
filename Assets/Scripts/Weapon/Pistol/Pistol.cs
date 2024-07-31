using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : Weapon
{
    public override event Action OnReload;
    public override event Action OnAmmoFull;
    public override event Action OnMakeShhot;
    public override event Action<int> OnLowAmmo;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (_input is not null)
        {
            _input.Gun.Shoot.performed += HandleShoot;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (_input is not null)
        {
            _input.Gun.Shoot.performed -= HandleShoot;
        }
    }

    private void HandleShoot(InputAction.CallbackContext context)
    {
        if (_isReloading)
        {
            return;
        }
    }

    protected override void Shoot()
    {
        
    }

    protected override void Reload()
    {

    }

    protected override void HandleReload(InputAction.CallbackContext context)
    {

    }
}
