using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlsyerWeaponController : MonoBehaviour
{
    private Weapon _currentWeapon;

    private MainInput _input;

    private PlayerInventoryController _playerInventoryController;

    [Inject]
    private void Initialize(MainInput input)
    {
        _input = input;
    }

    private void Awake()
    {
        _playerInventoryController = GetComponent<PlayerInventoryController>();

        if (_playerInventoryController is null)
        {
            Debug.LogError($"there's no inventroyController on gameObject {gameObject.name}!");
        }
    }

    private void OnEnable()
    {
        if (_input is not null)
        {
            _input.Enable();

            _input.Gun.Shoot.performed += HandleShoot;
            _input.Gun.Reload.performed += HandleReload;
        }

        if ( _playerInventoryController is not null)
        {
            _playerInventoryController.OnCurrentWeaponChanged += HandleWeaponChangings;
        }
    }

    private void OnDisable()
    {
        if (_input is not null)
        {
            _input.Disable();

            _input.Gun.Shoot.performed -= HandleShoot;
            _input.Gun.Reload.performed -= HandleReload;
        }

        if (_playerInventoryController is not null)
        {
            _playerInventoryController.OnCurrentWeaponChanged -= HandleWeaponChangings;
        }
    }

    private void HandleShoot(InputAction.CallbackContext context)
    {
        if (_currentWeapon is null)
        {
            return;
        }

        _currentWeapon.Shoot();
    }

    private void HandleReload(InputAction.CallbackContext context)
    {
        if (_currentWeapon is null)
        {
            return;
        }

        _currentWeapon.Reload();
    }

    private void HandleWeaponChangings(Weapon newWeapon)
    {
        _currentWeapon = newWeapon;
    }
}
