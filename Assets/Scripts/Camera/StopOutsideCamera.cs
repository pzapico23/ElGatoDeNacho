using Player;
using UnityEngine;

public class StopOutsideCamera : MonoBehaviour
{
    private Camera cam;
    private float halfWidth;
    private float halfHeight;

    [SerializeField] private float respawnGraceSeconds = 1f;
    private float graceTimer;

    void Start()
    {
        cam = Camera.main;

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider)
        {
            halfHeight = boxCollider.size.y * 0.5f;
        } else
        {
            halfHeight = 0f;
        }
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        pos.x = Mathf.Clamp(
            pos.x,
            bottomLeft.x ,
            topRight.x 
        );

        pos.y = Mathf.Clamp(
            pos.y,
            bottomLeft.y ,
            topRight.y
        );

        transform.position = pos;

        if (graceTimer > 0f)
        {
            graceTimer -= Time.deltaTime;
            return;
        }

        if (transform.position.y - halfHeight <= bottomLeft.y)
        {
            transform.GetComponent<Health>().Kill();
        }
    }

    public void NotifyRespawned()
    {
        graceTimer = respawnGraceSeconds;
    }

    public float getHalfHeight()
    {
        return halfHeight;
    }
}
