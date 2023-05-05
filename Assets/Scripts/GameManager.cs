using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameValues _defaultValues;
    [SerializeField] private GameValues _currentValues;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Transform _playerTransform;

    private void Awake()
    {
        _currentValues.score = _defaultValues.score;
        _playerTransform.position = _defaultValues.playerPosition;
    }

    private void Update()
    {
        _scoreText.text = $"SCORE: {_currentValues.score.ToString()}";
    }
}
