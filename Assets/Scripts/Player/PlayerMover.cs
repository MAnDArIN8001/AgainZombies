using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    public event Action<Vector2> OnMovementInputValuesCompute;

    [SerializeField] private float _forwardMovingSpeed;
    [SerializeField] private float _backwardMovingSpeed;

    private Vector2 _lastInputValues;

    private Rigidbody _rigidbody;

    private MainInput _mainInput;

    [Inject] 
    private void Initialize(MainInput input)
    {
        _mainInput = input;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _mainInput.Enable();
    }

    private void OnDisable()
    {
        _mainInput.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 inputValue = ReadInputValues();

        if (inputValue != _lastInputValues)
        {
            OnMovementInputValuesCompute?.Invoke(inputValue);
        }

        _lastInputValues = inputValue;

        Move(inputValue);
    }

    private void Move(Vector2 inputValue)
    {
        float speed = inputValue.y > 0 ? _forwardMovingSpeed : _backwardMovingSpeed;

        Vector3 moveDirection = transform.forward * inputValue.y * speed + transform.right * inputValue.x * speed;
        moveDirection.y = _rigidbody.velocity.y;

        _rigidbody.velocity = moveDirection;
    }

    private Vector2 ReadInputValues() => _mainInput.Player.Movement.ReadValue<Vector2>();
}
