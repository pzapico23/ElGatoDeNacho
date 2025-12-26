using UnityEngine;

public class BouncePlatform : MonoBehaviour
{

    [SerializeField] private float bounceForce;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for(int i = 0; i < collision.contactCount; i++)
            {
                collision.rigidbody.AddForce(-collision.GetContact(i).normal * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
