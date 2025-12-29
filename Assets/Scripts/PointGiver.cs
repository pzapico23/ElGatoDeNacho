using UnityEngine;

public class PointGiver : MonoBehaviour
{
    [SerializeField] private float points;

    public float pointsGiven
    {
        get { return points; }
        private set { points = value; }
    }
}
