using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameValues _currentValues;
    [SerializeField] private GameObject _winTextObject;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("isOpened", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Player.playerTag))
        {
            _currentValues.score += 100;
            _winTextObject.SetActive(true);
            _animator.SetBool("isOpened", true);
        }
    }

    private void Close()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
