using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform initialSpawnPoint;
    [SerializeField] private TextMeshProUGUI scoreGUI;
    [SerializeField] private Player.PlayerController playerController;
    [SerializeField] private RectTransform ballModeBar;
    [SerializeField] private Player.Health playerHealth;
    [SerializeField] private GameObject scorePopupPrefab;
    [SerializeField] private Canvas gameCanvas;

    private Transform currentSpawnPoint;
    private Vector3 lastGroundPosition;
    private int points = 0;
    private HeartsUI heartsUI;

    void Start()
    {
        heartsUI = gameCanvas.GetComponent<HeartsUI>();
        if (heartsUI && playerHealth)
        {
            heartsUI.InitalizeHearts(playerHealth.MaxHearts);
        }
    }

    void Update()
    {
        scoreGUI.text = points.ToString();
        float newWith = playerController.getCurrentBallMeter() / playerController.getMaxBallMeter() * 300;
        ballModeBar.sizeDelta = new Vector2(newWith, 30);

        if (heartsUI && playerHealth)
        {
            heartsUI.UpdateHearts(playerHealth.Hearts, playerHealth.MaxHearts);
        }
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

    public void addPoints(int points)
    {
        this.points += points;
    }

    public void ShowScorePopup(Vector3 worldPosition, int score)
    {
        if (scorePopupPrefab != null && gameCanvas != null)
        {
            GameObject popup = Instantiate(scorePopupPrefab, gameCanvas.transform);
            RectTransform popUpRect = popup.GetComponent<RectTransform>();
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

            Vector2 localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                gameCanvas.GetComponent<RectTransform>(),
                screenPosition,
                Camera.main,
                out localPosition
            );

            popUpRect.anchoredPosition = localPosition;
            
            ScorePopup scorePopup = popup.GetComponent<ScorePopup>();
            if (scorePopup != null)
            {
                scorePopup.SetScore(score);
            }

            Debug.Log($"Popup - World: {worldPosition}, Screen: {screenPosition}, Local: {localPosition}");

        }
    }
}
