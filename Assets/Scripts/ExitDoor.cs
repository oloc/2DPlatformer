using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameValues _currentValues;
    [SerializeField] private GameObject _winTextObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Player.playerTag))
        {
            _currentValues.score += 100;
            _winTextObject.SetActive(true);
        }
    }
}
