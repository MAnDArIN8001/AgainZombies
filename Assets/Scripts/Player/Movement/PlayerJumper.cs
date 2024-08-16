using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumper : MonoBehaviour
{
    public event Action OnJumped;
    public event Action<bool> OnGroundedStateChanged;

    private bool _isOnGround;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckDistance;

    [SerializeField] private LayerMask _groundableLayers; 

    private Rigidbody _rigidbody;

    private MainInput _input;

    [Inject] 
    private void Initialize(MainInput input)
    {
        _input = input;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Jump.performed += HandleJump;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Jump.performed -= HandleJump;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isOnGround 
            && Physics.Raycast(transform.position, -transform.up, _groundCheckDistance, _groundableLayers))
        {
            _isOnGround = true;

            OnGroundedStateChanged?.Invoke(_isOnGround);
        }
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if (!_isOnGround)
        {
            return;
        }

        OnJumped?.Invoke();

        Jump();
    }

    private void Jump()
    {
        Vector3 newVelocity = new Vector3()
        {
            x = _rigidbody.velocity.x,
            y = _jumpForce,
            z = _rigidbody.velocity.z
        };

        _rigidbody.velocity = newVelocity;

        _isOnGround = false;

        OnGroundedStateChanged?.Invoke(_isOnGround);
    }
}
