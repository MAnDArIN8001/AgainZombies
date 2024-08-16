using System;

public class Pistol : Weapon
{
    public override event Action OnReload;
    public override event Action OnAmmoFull;
    public override event Action OnMakeShhot;
    public override event Action<int> OnLowAmmo;
    public override event Action<bool> OnAimingStateChanged;

    public override void Shoot()
    {
        
    }

    public override void Reload()
    {

    }

    public override void HandleAiming(bool aimingStat)
    {
        
    }
}
