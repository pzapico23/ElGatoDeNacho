using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float damping;

    private Vector3 lastPosition;

    private bool isFollowing = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFollowing)
        {
            Vector3 targetPosition = player.position + offset;
            if (lastPosition.y < targetPosition.y)
            {
                targetPosition.z = transform.position.z;
                targetPosition.x = transform.position.x;
                Vector3 vel = Vector3.zero;
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
            }
            lastPosition = transform.position;
        }
    }

    public void SetIsFollowing(bool isFollowing)
    {
        this.isFollowing = isFollowing;
    }

    public void SnapToPlayer()
    {
        if (!player)
        {
            return;
        }

        Vector3 targetPosition = player.position + offset;
        targetPosition.z = transform.position.z;
        targetPosition.x = transform.position.x;

        transform.position = targetPosition;
        lastPosition = transform.position;
    }
}
