using UnityEngine;

public class PointGiver : MonoBehaviour
{
    [SerializeField] private float minPoints = 150f;
    [SerializeField] private float maxPoints = 500f;

    public int Points
    {
        get { return Mathf.RoundToInt(Random.Range(minPoints, maxPoints)); }
    }
}
