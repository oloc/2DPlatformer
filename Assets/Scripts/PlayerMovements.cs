using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform _groundCheckLeft;
    [SerializeField] private Transform _groundCheckRight;
    [SerializeField] private GameObject _playerLeft;
    [SerializeField] private GameObject _playerRight;
    [Header("Parameters")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _playerJumpForce;
    [SerializeField] private int _maxJump;

    // Private Elements
    private Rigidbody2D _rigidBody;
    private float _moveInput;
    private bool _jumpInput;
    private Vector2 _jumpVector = new Vector2(0.0f, 1.0f);
    private int _numberOfJump;

    // Flags
    private bool _isGrounded = false;

    private const string HORIZONTAL_INPUT_NAME = "Horizontal";
    private const string JUMP_INPUT_NAME = "Jump";

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerRight.SetActive(true);
        _playerLeft.SetActive(false);
        _numberOfJump = 0;
    }

    private void FixedUpdate()
    {
        // Horizontal move
        _rigidBody.velocity = new Vector2(_moveInput * _playerSpeed, _rigidBody.velocity.y);
    }

    private void JumpAction()
    {
        if (_numberOfJump < _maxJump)
        {
            _numberOfJump++;
            _isGrounded = false;
            _rigidBody.AddForce(_jumpVector * _playerJumpForce, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        _jumpInput = Input.GetButtonDown(JUMP_INPUT_NAME);
        _moveInput = Input.GetAxisRaw(HORIZONTAL_INPUT_NAME);

        // is grounded check only on fall
        if (_rigidBody.velocity.y < 0f)
        {
            _isGrounded = Physics2D.OverlapArea(_groundCheckRight.position, _groundCheckLeft.position);
        }
        if (_isGrounded)
        {
            _numberOfJump = 0;
        }
        if (_moveInput < 0)
        {
            _playerRight.SetActive(false);
            _playerLeft.SetActive(true);
        }
        else if (_moveInput > 0)
        {
            _playerRight.SetActive(true);
            _playerLeft.SetActive(false);
        }

        if (_jumpInput)
        {
            JumpAction();
        }
    }
}