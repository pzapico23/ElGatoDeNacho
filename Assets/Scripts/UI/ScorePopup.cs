using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float lifetime = 1.5f;
    
    private TextMeshProUGUI textMesh;
    private Color originalColor;
    private float timer = 0f;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        if (textMesh != null)
        {
            originalColor = textMesh.color;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        
        if (textMesh != null && timer > (lifetime - fadeDuration))
        {
            // Calcula el tiempo que ha pasado desde que comienza el fade, lo normaliza y ajusta la opacidad
            float alpha = 1f - ((timer - (lifetime - fadeDuration)) / fadeDuration);
            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }
        
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    public void SetScore(int score)
    {
        if (textMesh != null)
        {
            textMesh.text = "+" + score.ToString();
        }
    }
}