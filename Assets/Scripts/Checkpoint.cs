using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] GameValues _defaultValues;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Player.playerTag))
        {
            _defaultValues.playerPosition = transform.position;
        }
    }
}
