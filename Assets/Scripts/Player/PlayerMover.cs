using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    public event Action<Vector2> OnMovementInputValuesCompute;
    public event Action<bool> OnRunningStateChanged;

    private bool _isRunning;

    [SerializeField] private float _forwardMovingSpeed;
    [SerializeField] private float _forwardRuningSpeed;
    [SerializeField] private float _backwardMovingSpeed;
    [SerializeField] private float _backwardRuningSpeed;

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

        if (_rigidbody is null)
        {
            Debug.LogError($"There's no rigidbody on game object: {gameObject.name}");
        }

        if (_mainInput is null)
        {
            Debug.LogError("the input system wasnt initialize");
        } 
    }

    private void OnEnable()
    {
        _mainInput.Enable();
        _mainInput.Player.Run.performed += HandleRun;
        _mainInput.Player.Run.canceled += HandleRun;
    }

    private void OnDisable()
    {
        _mainInput.Disable();
        _mainInput.Player.Run.performed -= HandleRun;
        _mainInput.Player.Run.canceled -= HandleRun;
    }

    private void FixedUpdate()
    {
        Vector2 inputValue = ReadInputValues();

        if (inputValue != _lastInputValues)
        {
            OnMovementInputValuesCompute?.Invoke(inputValue.normalized);
        }

        _lastInputValues = inputValue;

        Move(inputValue);
    }

    private void Move(Vector2 inputValue)
    {
        float speed = ComputeSpeed(inputValue);

        Vector3 moveDirection = transform.forward * inputValue.y * speed + transform.right * inputValue.x * speed;
        moveDirection.y = _rigidbody.velocity.y;

        _rigidbody.velocity = moveDirection;
    }

    private float ComputeSpeed(Vector2 inputValue)
    {
        bool isMovingForward = inputValue.y > 0;

        return isMovingForward
            ? _isRunning ? _forwardRuningSpeed : _forwardMovingSpeed
            : _isRunning ? _backwardRuningSpeed : _backwardMovingSpeed;
    }

    private void HandleRun(InputAction.CallbackContext context)
    {
        _isRunning = context.ReadValueAsButton();

        OnRunningStateChanged?.Invoke(_isRunning);
        OnMovementInputValuesCompute?.Invoke(_lastInputValues.normalized);
    }

    private Vector2 ReadInputValues() => _mainInput.Player.Movement.ReadValue<Vector2>();
}
