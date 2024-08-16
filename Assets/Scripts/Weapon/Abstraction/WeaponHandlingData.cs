using UnityEngine;

public class WeaponHandlingData : MonoBehaviour
{
    [SerializeField] private WeaponHandlingType _defaultHandlingType;
    [SerializeField] private WeaponHandlingType _aimingHandlingType;

    [SerializeField] private Transform _rightHandPoint;
    [SerializeField] private Transform _leftHandPoint;

    public WeaponHandlingType DefaultHandlingType => _defaultHandlingType;
    public WeaponHandlingType AimingHandlingType => _aimingHandlingType;

    public Transform RightHandPoint => _rightHandPoint;
    public Transform LeftHandPoint => _leftHandPoint;
}
