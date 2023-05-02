using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    [SerializeField]
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 nextPosition = player.transform.position + new Vector3(0, 1, -10);
        transform.position = Vector3.Lerp(currentPosition, nextPosition, 5);
    }
}
