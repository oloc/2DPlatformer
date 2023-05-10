using UnityEngine;

public class Player : MonoBehaviour
{
    // On d�clare nos variables priv�es s�rialis�es
    [SerializeField]
    private GroundChecker _groundChecker;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private int _jumpCountMax = 2;
    [SerializeField]
    private float _fallGravityScale = 3f;

    // On d�clare nos composants
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Animator _animator;

    // On d�clare nos variables priv�es
    private int _jumpCount;
    private float _movementInput;
    private bool _jumpInput;

    public const string playerTag = "Player";

    private void Awake()
    {
        // On r�cup�re les composants
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
    }

    // RAPPEL : ON RECUPERE TOUJOURS LES INPUTS EN update (NON EN FixedUpdate)
    private void Update()
    {
        // On r�cup�re les inputs horizontales
        GetHorizontalInput();
        // On r�cup�re l'input de saut
        GetJumpInput();
        // On g�re le changement de cot� du personnage (gauche / droite)
        TurnCharacter();
        // On envoie les param�tres necessaires � l'Animator
        SetAnimatorParameters();
    }

    private void FixedUpdate()
    {
        // On applique les inputs horizontales
        ApplyHorizontalInput();
        // On am�liore la chute
        BetterFalling();
    }

    // Retourne s'il reste des sauts au joueur ou non
    public bool CanJump()
    {
        return (_jumpCount < _jumpCountMax && _jumpInput == true );
    }

    // Retourne la v�locit� horizontale
    public float GetHorizontalVelocity()
    {
        return _rigidbody.velocity.x;
    }

    // Retourne la velocit� verticale
    public float GetVerticalVelocity()
    {
        return _rigidbody.velocity.y;
    }

    // Retourne s'il y a contact avec le sol ou non
    public bool IsGrounded()
    {
        return _groundChecker.IsGrounded();
    }

    // R�initialise le compteur de sauts
    public void ResetJumpCounter()
    {
        _jumpCount = 0;
    }

    // Fais sauter le joueur
    public void Jump()
    {
        _rigidbody.velocity = Vector2.up * _jumpForce;
        _jumpCount++;
    }


    private void GetHorizontalInput()
    {
        // On stocke l'input horizontale dans une variable globale (pour pouvoir appliquer le mouvement en FixedUpdate)
        _movementInput = Input.GetAxisRaw("Horizontal");
    }

    private void GetJumpInput()
    {
        _jumpInput = Input.GetButtonDown("Jump");
    }

    private void TurnCharacter()
    {
        // Selon la direction du personnage on le "retourne"
        if (_movementInput < 0)
        {
            _transform.right = Vector2.left;
        }
        else if (_movementInput > 0)
        {
            _transform.right = Vector2.right;
        }
    }

    private void ApplyHorizontalInput()
    {
        // On applique le mouvement horizontal
        _rigidbody.velocity = new Vector2(_movementInput * _speed, _rigidbody.velocity.y);  // Ne pas oublier de reporter la vitesse verticale actuelle du rigidbody pour �viter l'effet "gravit� lunaire"
    }

    private void BetterFalling()
    {
        // On modifie l'echelle de gravit� si le personnage est en train de tomber
        _rigidbody.gravityScale = _rigidbody.velocity.y < 0f ? _fallGravityScale : 1f;
    }

    private void SetAnimatorParameters()
    {
        // On envoie les param�tres � l'Animator
        _animator.SetFloat("HorizontalSpeed", Mathf.Abs(_movementInput));   // On "retire le signe" de la valeur de notre input horizontal pour obtenir une vitesse horizontale absolue et normalis�e (valeur comprise entre 0 et 1)
    }

    // "Parenting" du joueur au contact des plateformes mobiles
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mobile"))
        {
            _transform.parent = collision.transform;
        }
    }
    // "Unparenting" du joueur en sortie contact avec des plateformes mobiles
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mobile"))
        {
            _transform.parent = null;
        }
    }
}