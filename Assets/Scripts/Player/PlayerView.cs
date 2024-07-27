using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerRotator))]
public class PlayerView : MonoBehaviour
{
    private bool _isRunning;

    [SerializeField] private float _rotationSoftZone;
    [SerializeField] private float _runingStateValue;

    private Animator _animator;

    private PlayerMover _playerMover;
    private PlayerRotator _playerRotator;
    private PlayerJumper _playerJumper;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _playerMover = GetComponent<PlayerMover>();
        _playerRotator = GetComponent<PlayerRotator>();
        _playerJumper = GetComponent<PlayerJumper>();

        if (_animator is null)
        {
            Debug.LogError("Animator wasnt initialize");
        }

        if (_playerMover is null)
        {
            Debug.LogError($"There's no PlayerMover on game object: {gameObject.name}");
        }

        if (_playerJumper is null)
        {
            Debug.LogError($"There's no PlayerJumper on game object: {gameObject.name}");
        }

        if (_playerRotator is null)
        {
            Debug.LogError($"There's no PlayerRotator on game object: {gameObject.name}");
        }
    }

    private void OnEnable()
    {
        if (_playerMover is not null)
        {
            _playerMover.OnMovementInputValuesCompute += HandleMovement;
            _playerMover.OnRunningStateChanged += HandleRun;
        }

        if (_playerRotator is not null)
        {
            _playerRotator.OnRotatingInputValuesCompute += HandleRotating;
        }

        if (_playerJumper is not null)
        {
            _playerJumper.OnJumped += HandleJump;
            _playerJumper.OnGroundedStateChanged += HandleLanding;
        }
    }

    private void OnDisable()
    {
        if (_playerMover is not null)
        {
            _playerMover.OnMovementInputValuesCompute -= HandleMovement;
            _playerMover.OnRunningStateChanged -= HandleRun;
        }

        if (_playerRotator is not null)
        {
            _playerRotator.OnRotatingInputValuesCompute -= HandleRotating;
        }

        if (_playerJumper is not null)
        {
            _playerJumper.OnJumped -= HandleJump;
            _playerJumper.OnGroundedStateChanged -= HandleLanding;
        }
    }

    private void HandleMovement(Vector2 movementValue)
    {
        float computedMovementValue = movementValue.normalized.y;

        if (_isRunning)
        {
            computedMovementValue *= _runingStateValue;
        }

        _animator.SetFloat(PlayerAnimationsConsts.MovementValueKey, computedMovementValue);
    }

    private void HandleRotating(Vector2 rotatingValue)
    {
        if (Mathf.Abs(rotatingValue.y) < _rotationSoftZone)
        {
            _animator.SetFloat(PlayerAnimationsConsts.RotatingValueKey, 0);

            return;
        }

        _animator.SetFloat(PlayerAnimationsConsts.RotatingValueKey, rotatingValue.normalized.y);
    }

    private void HandleRun(bool isRuning)
    {
        _isRunning = isRuning;
    }

    private void HandleJump()
    {
        _animator.SetTrigger(PlayerAnimationsConsts.JumpKey);
    }

    private void HandleLanding(bool newStateValue)
    {
        _animator.SetBool(PlayerAnimationsConsts.GroundStateKey, newStateValue);
    }
}
