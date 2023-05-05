using System.Collections;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameValues _currentValues;
    [SerializeField] private GameObject _winTextObject;
    [SerializeField] private float _waitBeforeClose;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Player.playerTag))
        {
            _currentValues.score += 100;
            _winTextObject.SetActive(true);
            StartCoroutine(Close(_waitBeforeClose));
        }
    }

    private IEnumerator Close(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
