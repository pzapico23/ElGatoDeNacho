using UnityEngine;

public class BouncePlatform : MonoBehaviour
{

    [SerializeField] private float bounceForce;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //voy a intuir que es aquí donde tendría que lanzar el sonido de rebote

            if (GetComponent<SoundManager>() != null)
            {
                GetComponent<SoundManager>().PlaySound("Boing", 0.4f, 0.05f, 0.2f);
            }

            for (int i = 0; i < collision.contactCount; i++)
            {
                collision.rigidbody.AddForce(-collision.GetContact(i).normal * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
