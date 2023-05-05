using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private GameValues _defaultValues;
    [SerializeField] private Transform _playerTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag(Player.playerTag))
        {
            _playerTransform.position = _defaultValues.playerPosition;
        }
    }
}
