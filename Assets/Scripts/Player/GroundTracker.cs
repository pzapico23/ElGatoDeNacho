using UnityEngine;

public class GroundTracker : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float platformCheckWidth = 1.5f;
    [SerializeField] private float groundCheckDistance = 1.5f;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (GetComponent<PlayerMovement>().IsGrounded && IsSafePosition())
        {
            if (gameManager)
            {
                gameManager.LastGroundPosition = transform.position;
            }
        }
    }

    bool IsSafePosition()
    {
        Vector2 leftCheck = (Vector2)transform.position + Vector2.left * (platformCheckWidth / 2f);
        Vector2 rightCheck = (Vector2)transform.position + Vector2.right * (platformCheckWidth / 2f);

        RaycastHit2D leftHit = Physics2D.Raycast(leftCheck, Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCheck, Vector2.down, groundCheckDistance, groundLayer);

        return leftHit.collider && rightHit.collider;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = IsSafePosition() ? Color.green : Color.red;

        Vector2 leftCheck = (Vector2)transform.position + Vector2.left * (platformCheckWidth / 2f);
        Vector2 rightCheck = (Vector2)transform.position + Vector2.right * (platformCheckWidth / 2f);

        Gizmos.DrawLine(leftCheck, leftCheck + Vector2.down * groundCheckDistance);
        Gizmos.DrawLine(rightCheck, rightCheck + Vector2.down * groundCheckDistance);
        Gizmos.DrawLine(leftCheck, rightCheck);
    }
}
