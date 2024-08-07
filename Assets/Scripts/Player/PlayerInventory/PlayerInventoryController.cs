using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInventoryController : InventoryController
{
    public event Action<WeaponType> OnCurrentWeaponChanged;

    private MainInput _input;

    [Inject] 
    private void Initialize(MainInput input, PlayerInventoryModel inventoryModel)
    {
        _input = input;
        _inventoryModel = inventoryModel;
    }

    private void OnEnable()
    {
        if (_input is not null)
        {
            _input.Enable();

            _input.Inventory.LightWeapon.performed += SwapOnLightWeapon;
            _input.Inventory.CommonWeapon.performed += SwapOnCommonWeapon;
            _input.Inventory.HeavyWeapon.performed += SwapOnHeavyWeapon;
            _input.Inventory.Grenade.performed += SwapOnGreande;
        }
    }

    private void OnDisable()
    {
        if (_input is not null)
        {
            _input.Disable();

            _input.Inventory.LightWeapon.performed -= SwapOnLightWeapon;
            _input.Inventory.CommonWeapon.performed -= SwapOnCommonWeapon;
            _input.Inventory.HeavyWeapon.performed -= SwapOnHeavyWeapon;
            _input.Inventory.Grenade.performed += SwapOnGreande;
        }
    }

    private void SwapOnLightWeapon(InputAction.CallbackContext context)
    {
        SwapCyurrentWeapon(WeaponType.LightWeapon);
    }

    private void SwapOnCommonWeapon(InputAction.CallbackContext context) 
    {
        SwapCyurrentWeapon(WeaponType.CommonWeapon);
    }

    private void SwapOnHeavyWeapon(InputAction.CallbackContext context)
    {
        SwapCyurrentWeapon(WeaponType.HeavyWeapon);
    }

    private void SwapOnGreande(InputAction.CallbackContext context)
    {
        SwapCyurrentWeapon(WeaponType.Grenade);
    }

    private void SwapCyurrentWeapon(WeaponType weaponType)
    {
        GameObject weapon = _inventoryModel.GetWeaponByType(weaponType);

        _currentWeapon = weapon;
        OnCurrentWeaponChanged?.Invoke(weaponType);

        Debug.Log($"current weapon is: {weapon}, of type: {weaponType}");
    }
}
