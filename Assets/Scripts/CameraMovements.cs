using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform player;
    [Header("Parameters")]
    [SerializeField] private float _followSharpness;

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 nextPosition = player.transform.position + new Vector3(0, 1, -10);
        transform.position = Vector3.Lerp(currentPosition, nextPosition, _followSharpness * Time.deltaTime);
    }
}
