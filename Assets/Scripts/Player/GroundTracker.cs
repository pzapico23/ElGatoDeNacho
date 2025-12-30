using UnityEngine;

public class GroundTracker : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LayerMask groundLayer;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            if (gameManager)
            {
                gameManager.LastGroundPosition = transform.position;
            }
        }
    }
}
