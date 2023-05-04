using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform _groundCheckLeft;
    [SerializeField] private Transform _groundCheckRight;
    [SerializeField] private LayerMask _layerMask;
    [Header("Parameters")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _playerJumpForce;
    [SerializeField] private int _maxJump;

    // Private Elements
    private Rigidbody2D _rigidBody;
    private Transform _transform;
    private Animator _animator;
    private float _moveInput;
    private bool _jumpInput;
    private int _numberOfJump;

    // Flags
    private bool _isGrounded = false;

    private const string HORIZONTAL_INPUT_NAME = "Horizontal";
    private const string JUMP_INPUT_NAME = "Jump";

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _transform = transform;
        _numberOfJump = 0;
    }

    private void FixedUpdate()
    {
        // Horizontal move
        _rigidBody.velocity = new Vector2(_moveInput * _playerSpeed, _rigidBody.velocity.y);
    }

    // Update is called once per frame
    private void Update()
    {
        _jumpInput = Input.GetButtonDown(JUMP_INPUT_NAME);
        _moveInput = Input.GetAxisRaw(HORIZONTAL_INPUT_NAME);

        if (Mathf.Abs(_moveInput) > 0)
        {
            _animator.SetBool("isIdle", false);
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isIdle", true);
            _animator.SetBool("isWalking", false);
        }

        TurnCharacter();
        CheckGrounded();
        JumpAction();
    }

    private void CheckGrounded()
    {
        // is grounded check only on fall
        if (_rigidBody.velocity.y < 0f)
        {
            _animator.SetBool("isFalling", true);
            _isGrounded = Physics2D.OverlapArea(_groundCheckRight.position, _groundCheckLeft.position, _layerMask);
            if (_isGrounded)
            {
                _numberOfJump = 0;
            }
        }
        else
        {
            _animator.SetBool("isFalling", false);
        }
    }
    private void JumpAction()
    {
        if (_jumpInput && _numberOfJump < _maxJump)
        {
            _numberOfJump++;
            _isGrounded = false;
            _rigidBody.AddForce(Vector2.up * _playerJumpForce, ForceMode2D.Impulse);
        }
        if (_rigidBody.velocity.y > 0f)
        {
            _animator.SetBool("isIdle", false);
            _animator.SetBool("isJumping", true);
        }
        else
        {
            _animator.SetBool("isJumping", false);
        }
    }

    private void TurnCharacter()
    {
        if (_moveInput < 0)
        {
            _transform.right = Vector2.left;
        }
        else if (_moveInput > 0)
        {
            _transform.right = Vector2.right;
        }
    }
}