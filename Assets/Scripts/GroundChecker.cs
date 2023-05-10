using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    // On déclare nos variables privées sérialisées
    [SerializeField]
    private Transform _topLeft;
    [SerializeField]
    private Transform _botRight;
    [SerializeField]
    private LayerMask _layerMask;

    public bool IsGrounded()
    {
        // On récupère une eventuelle collision (Overlap)
        Collider2D collider = Physics2D.OverlapArea(_topLeft.position, _botRight.position, _layerMask);

        // On retourne s'il y a collision avec un sol
        return collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        // On dessine (en Debug) le rectangle formé par les deux points pour une meilleure visualisation
        Debug.DrawLine(_topLeft.position, new Vector2(_botRight.position.x, _topLeft.position.y));
        Debug.DrawLine(new Vector2(_botRight.position.x, _topLeft.position.y), _botRight.position);
        Debug.DrawLine(_botRight.position, new Vector2(_topLeft.position.x, _botRight.position.y));
        Debug.DrawLine(new Vector2(_topLeft.position.x, _botRight.position.y), _topLeft.position);
    }
}