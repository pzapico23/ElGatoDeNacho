using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;
    [SerializeField] private PhysicsMaterial2D bounceMaterial;
    [SerializeField] private float boundaryWidth = 100f;
    [SerializeField] private float boundaryThickness = 1f;

    private Vector3 lastPosition;
    private Vector3 initialPosition;
    private bool isFollowing = true;
    private GameObject topBoundaryObject;

    void Start()
    {
        initialPosition = transform.position;    
    }

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

        if (!isFollowing)
        {
            CreateTopBoundary();
        } else
        {
            DestroyTopBoundary();
        }
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

    public void ResetToInitialPosition()
    {
        transform.position = initialPosition;
        lastPosition = initialPosition;
    }

    void CreateTopBoundary()
    {
        topBoundaryObject = new GameObject("TopCameraBoundary");
        topBoundaryObject.layer = LayerMask.NameToLayer("Boundary");

        BoxCollider2D topBoundaryCollider = topBoundaryObject.AddComponent<BoxCollider2D>();
        topBoundaryCollider.size = new Vector2(boundaryWidth, boundaryThickness);

        if (bounceMaterial != null)
        {
            topBoundaryCollider.sharedMaterial = bounceMaterial;
        }

        topBoundaryObject.transform.position = new Vector3(
            transform.position.x,
            transform.position.y + Camera.main.orthographicSize,
            0f
        );
    }

    void DestroyTopBoundary()
    {
        if (topBoundaryObject != null)
        {
            Destroy(topBoundaryObject);
        }
    }
}
