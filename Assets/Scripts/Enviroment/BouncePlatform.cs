using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 currentVelocity = collision.rigidbody.linearVelocity;
            collision.rigidbody.AddForce(new Vector2(currentVelocity.x, -currentVelocity.y));
        }
    }
}
