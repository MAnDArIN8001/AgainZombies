using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInventoryController : InventoryController
{
    public event Action<Weapon> OnCurrentWeaponChanged;
    public event Action OnHandsEmpty;

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
            _input.Inventory.Grenade.performed -= SwapOnGreande;
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
        Weapon weaponPrefab = _inventoryModel.GetWeaponByType(weaponType);

        if (_currentWeapon?.WeaponType == weaponType)
        {
            OnHandsEmpty?.Invoke();

            Destroy(_currentWeapon.gameObject);

            _currentWeapon = null;

            return;
        }

        if (_currentWeapon is not null)
        {
            Destroy(_currentWeapon.gameObject);

            _currentWeapon = null;
        }

        Weapon weapon = CreateWeapon(weaponPrefab.gameObject);

        _currentWeapon = weapon;
        OnCurrentWeaponChanged?.Invoke(weapon);

        Debug.Log($"current weapon is: {weapon}, of type: {weaponType}");
    }

    private Weapon CreateWeapon(GameObject weaponPrefab)
    {
        GameObject weapon = Instantiate(weaponPrefab);

        return weapon.GetComponent<Weapon>();
    }
}
