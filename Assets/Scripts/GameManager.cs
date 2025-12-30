using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform initialSpawnPoint;
    [SerializeField] private TextMeshProUGUI scoreGUI;
    [SerializeField] private Player.PlayerController playerController;
    [SerializeField] private RectTransform ballModeBar;
    private Transform currentSpawnPoint;
    private Vector3 lastGroundPosition;
    private float points = 0;

    private void Update()
    {
        scoreGUI.text = points.ToString();
        float newWith = playerController.getCurrentBallMeter() / playerController.getMaxBallMeter() * 300;
        ballModeBar.sizeDelta = new Vector2(newWith, 30);
    }
    void Awake()
    {
        if (!initialSpawnPoint)
            Debug.LogError("No se ha asignado un punto de spawn inicial");

        currentSpawnPoint = initialSpawnPoint;
        lastGroundPosition = initialSpawnPoint.position;
    }

    public Transform CurrentSpawnPoint
    {
        get { return currentSpawnPoint; }
        private set { currentSpawnPoint = value; }
    }

    public Vector3 LastGroundPosition
    {
        get { return lastGroundPosition; }
        set { lastGroundPosition = value; }
    }

    public void addPoints(float points)
    {
        this.points += points;
    }
}
