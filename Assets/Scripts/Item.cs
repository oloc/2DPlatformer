using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private GameValues gameValues;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Player"))
        {
            gameValues.score ++;
            Destroy(gameObject);
        }
    }
}
