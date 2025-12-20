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

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        if(lastPosition.x < targetPosition.x)
        {
            targetPosition.z = transform.position.z;
            Vector3 vel = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
        }
        lastPosition = transform.position;
    }
}
