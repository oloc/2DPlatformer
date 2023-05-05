using UnityEngine;

public class Column : MonoBehaviour
{
    [SerializeField] private Sprite _spriteHealth1;
    [SerializeField] private Sprite _spriteHealth2;
    [SerializeField] private Sprite _spriteHealth3;

    private SpriteRenderer _spriteRenderer;
    private int _health;
    private bool _hurted;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _health = 3;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Start: {_health}");
    }

    // Update is called once per frame
    private void Update()
    {
        ChangeSpriteByHealth(_health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Player.playerTag) && !_hurted)
        {
            _health--;
            _hurted = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _hurted = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _hurted = true;
    }

    private void ChangeSpriteByHealth(int health)
    {
        switch (health)
        {
            case 3:
                _spriteRenderer.sprite = _spriteHealth3;
                break;
            case 2:
                _spriteRenderer.sprite = _spriteHealth2;
                break;
            case 1:
                _spriteRenderer.sprite = _spriteHealth1;
                break;
            case 0:
                Destroy(gameObject);
                break;
        }
    }
}
