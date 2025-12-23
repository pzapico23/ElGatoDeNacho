using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform initialSpawnPoint;
    private Transform currentSpawnPoint;

    void Awake()
    {
        if (!initialSpawnPoint)
            Debug.LogError("No se ha asignado un punto de spawn inicial");

        currentSpawnPoint = initialSpawnPoint;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform CurrentSpawnPoint
    {
        get { return currentSpawnPoint; }
        private set { currentSpawnPoint = value; }
    }
}
