using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerRotator))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private float _rotationSoftZone;

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
    }

    private void OnEnable()
    {
        _playerMover.OnMovementInputValuesCompute += HandleMovement;
        _playerRotator.OnRotatingInputValuesCompute += HandleRotating;
        _playerJumper.OnJumped += HandleJump;
        _playerJumper.OnGroundedStateChanged += HandleLanding;
    }

    private void OnDisable()
    {
        _playerMover.OnMovementInputValuesCompute -= HandleMovement;
        _playerRotator.OnRotatingInputValuesCompute -= HandleRotating;
        _playerJumper.OnJumped -= HandleJump;
        _playerJumper.OnGroundedStateChanged -= HandleLanding;
    }

    private void HandleMovement(Vector2 movementValue)
    {
        _animator.SetFloat(PlayerAnimationsConsts.MovementValueKey, movementValue.y);
    }

    private void HandleRotating(Vector2 rotatingValue)
    {
        if (Mathf.Abs(rotatingValue.x) < _rotationSoftZone)
        {
            _animator.SetFloat(PlayerAnimationsConsts.RotatingValueKey, 0);

            return;
        }

        _animator.SetFloat(PlayerAnimationsConsts.RotatingValueKey, rotatingValue.normalized.x);
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
