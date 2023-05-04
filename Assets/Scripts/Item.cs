using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private GameValues gameValues;
    [SerializeField] private int points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Player"))
        {
            gameValues.score += points;
            Destroy(gameObject);
        }
    }
}
