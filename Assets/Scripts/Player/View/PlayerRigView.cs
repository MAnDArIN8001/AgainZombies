using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerRigView : MonoBehaviour
{
    [SerializeField] private Transform _defaultWeaponPoint;
    [SerializeField] private Transform _aimingWeaponPoint;
    [SerializeField] private Transform _rightHandTarget;
    [SerializeField] private Transform _leftHandTarget;
    [SerializeField] private Transform _ikRightHandPosition;
    [SerializeField] private Transform _ikLeftHandPosition;
    private Transform _currentWeaonPoint;

    [SerializeField] private TwoBoneIKConstraint _rightHandIK;
    [SerializeField] private TwoBoneIKConstraint _leftHandIK;

    private PlayerInventoryController _playerInventoryController;

    private Weapon _currentWeapon;

    private void Awake()
    {
        _playerInventoryController = GetComponent<PlayerInventoryController>();

        if (_playerInventoryController is null)
        {
            Debug.LogError($"The game object {gameObject} doesnt contains a PlayerInventoryController component");
        }
    }

    private void OnEnable()
    {
        if (_playerInventoryController is not null)
        {
            _playerInventoryController.OnCurrentWeaponChanged += HandleWeaponChangings;
            _playerInventoryController.OnHandsEmpty += HandleOnHandsEmpty; 
        }
    }

    private void OnDisable()
    {
        if (_playerInventoryController is not null)
        {
            _playerInventoryController.OnCurrentWeaponChanged -= HandleWeaponChangings;
            _playerInventoryController.OnHandsEmpty -= HandleOnHandsEmpty;
        }
    }

    private void Update()
    {
        if (_currentWeapon is not null)
        {
            _currentWeapon.transform.position = _currentWeaonPoint.position;
            _currentWeapon.transform.rotation = _currentWeaonPoint.rotation;

            _leftHandTarget.position = _ikLeftHandPosition.position;
            _rightHandTarget.position = _ikRightHandPosition.position;
        }
    }

    private void HandleWeaponAimingStateChangings(bool aimingState)
    {
        _currentWeaonPoint = aimingState ? _aimingWeaponPoint : _defaultWeaponPoint;

        SetupHandsIK();
    }

    private void HandleWeaponChangings(Weapon newWeapon)
    {
        _currentWeapon = newWeapon;
        _currentWeaonPoint = _defaultWeaponPoint;

        _currentWeapon.transform.parent = gameObject.transform;

        SetupHandsIK();

        SubscribeOnWeaponEvents();
    }

    private void HandleOnHandsEmpty() 
    {
        _currentWeapon = null;
        _ikRightHandPosition = null;
        _ikLeftHandPosition = null;

        _rightHandIK.weight = 0f;
        _leftHandIK.weight = 0f;
    }

    private void SubscribeOnWeaponEvents()
    {
        _currentWeapon.OnAimingStateChanged += HandleWeaponAimingStateChangings;
    }

    private void SetupHandsIK()
    {
        if (_currentWeapon.gameObject.TryGetComponent<WeaponHandlingData>(out var weaponHadlingData))
        {
            WeaponHandlingType handlingType = weaponHadlingData.DefaultHandlingType;

            _rightHandIK.weight = 1f;
            _leftHandIK.weight = handlingType == WeaponHandlingType.OneHanded ? 0f : 1f;

            _ikLeftHandPosition = weaponHadlingData.RightHandPoint;
            _ikRightHandPosition = weaponHadlingData.LeftHandPoint;  
        }
    }
}
