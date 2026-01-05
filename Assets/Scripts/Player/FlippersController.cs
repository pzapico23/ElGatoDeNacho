using UnityEngine;

public class FlippersController : MonoBehaviour
{

    public enum FlipperSide { Left, Right }

    [SerializeField] private FlipperSide side;
    [SerializeField] private float motorSpeedUp = 1000f;
    [SerializeField] private float motorSpeedDown = 1000f;
    [SerializeField] private float motorTorque = 10000f;

    private bool flipperActivated = false;
    private HingeJoint2D hinge;
    private JointMotor2D motor;

    void Awake()
    {
        hinge = GetComponent<HingeJoint2D>();
        motor = hinge.motor;
        this.enabled = false;
    }

    public void changeState(bool v)
    {
        flipperActivated = v;

        //si lo activo, sonido
        if (v)
        {
            if (GetComponent<SoundManager>() != null)
            {
                GetComponent<SoundManager>().PlaySound("Palanca", 1f, 0.05f, 0.1f);
            }
        }
    }

    public void OnEnable()
    {
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<PolygonCollider2D>().enabled = true;
    }

    public void OnDisable()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
    }

    void Update()
    {

        float direction = (side == FlipperSide.Left) ? -1f : 1f;
        if (flipperActivated)
        {
            motor.motorSpeed = motorSpeedUp * direction;
        }
        else
        {
            motor.motorSpeed = -motorSpeedDown * direction;
        }

        motor.maxMotorTorque = motorTorque;
        hinge.motor = motor;
    }
}
