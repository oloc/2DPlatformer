using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameValues _currentValues;
    [SerializeField] private Text _scoreText;

    private void Awake()
    {
        _currentValues.score = 0;
    }

    private void Update()
    {
        _scoreText.text = $"SCORE: {_currentValues.score.ToString()}";
    }
}
