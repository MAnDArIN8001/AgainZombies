using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerRotator))]
public class PlayerView : MonoBehaviour
{
    private Animator _animator;

    private PlayerMover _playerMover;
    private PlayerRotator _playerRotator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _playerMover = GetComponent<PlayerMover>();
        _playerRotator = GetComponent<PlayerRotator>();
    }

    private void OnEnable()
    {
        _playerMover.OnMovementInputValuesCompute += HandleMovement;
        _playerRotator.OnRotatingInputValuesCompute += HandleRotating;
    }

    private void OnDisable()
    {
        _playerMover.OnMovementInputValuesCompute -= HandleMovement;
        _playerRotator.OnRotatingInputValuesCompute -= HandleRotating;
    }

    private void HandleMovement(Vector2 movementValue)
    {
        _animator.SetFloat(PlayerAnimationsConsts.MovementValueKey, movementValue.y);
    }

    private void HandleRotating(Vector2 rotatingValue)
    {
        _animator.SetFloat(PlayerAnimationsConsts.RotatingValueKey, rotatingValue.x);
    }
}
