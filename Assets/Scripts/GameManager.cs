using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform initialSpawnPoint;
    [SerializeField] private TextMeshProUGUI scoreGUI;
    private Transform currentSpawnPoint;
    private float points = 0;

    private void Update()
    {
        scoreGUI.text = points.ToString();
    }
    void Awake()
    {
        if (!initialSpawnPoint)
            Debug.LogError("No se ha asignado un punto de spawn inicial");

        currentSpawnPoint = initialSpawnPoint;
    }

    public Transform CurrentSpawnPoint
    {
        get { return currentSpawnPoint; }
        private set { currentSpawnPoint = value; }
    }

    public void addPoints(float points)
    {
        this.points += points;
    }
}
