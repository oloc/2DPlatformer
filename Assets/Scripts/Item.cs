using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private GameValues _gameValues;
    [SerializeField] private int _itemPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Player"))
        {
            _gameValues.score += _itemPoints;
            Destroy(gameObject);
        }
    }
}
