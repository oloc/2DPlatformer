using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform _startMarker;
    [SerializeField] private Transform _endMarker;
    [Header("Parameters")]
    [SerializeField] private float _speed;

    private Transform _transform;
    private bool _way = true;

    private void Awake()
    {
        _transform = transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_way)
        {
            MoveTo(_endMarker.position);
        }
        else
        {
            MoveTo(_startMarker.position);
        }
    }

    private void MoveTo(Vector3 endPosition)
    {
        Vector3 currentPosition = transform.position;
        _transform.position = Vector3.Lerp(currentPosition, endPosition, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == _startMarker || other.transform == _endMarker)
        {
            _way = !_way;
        }
    }
}
