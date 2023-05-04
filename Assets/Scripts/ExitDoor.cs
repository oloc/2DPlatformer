using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameValues _currentValues;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            _currentValues.score += 100;
            Debug.Log("Win the game!");
        }
    }
}
