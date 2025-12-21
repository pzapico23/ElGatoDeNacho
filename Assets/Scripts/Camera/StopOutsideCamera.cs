using UnityEngine;

public class StopOutsideCamera : MonoBehaviour
{
    private Camera cam;
    private float halfWidth;
    private float halfHeight;

    void Start()
    {
        cam = Camera.main;

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
    }
}
