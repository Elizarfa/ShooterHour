using UnityEngine;
using UnityEngine.UI; // Для работы с UI Text
using TMPro; // Если вы используете TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Статический экземпляр GameManager для Singleton

    public GameObject targetPrefab;
    public float spawnRate = 1f;
    public Transform spawnArea; // Определите область, где могут появляться цели
    public int score = 0;
    public Text scoreTextLegacy; // Для UI Text
    public TextMeshProUGUI scoreTextPro; // Для TextMeshPro

    private float nextSpawnTime = 0f;

    void Awake()
    {
        // Реализация Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Поиск текстового объекта для отображения счета
        scoreTextPro = FindObjectOfType<TextMeshProUGUI>();
        if (scoreTextPro == null)
        {
            scoreTextLegacy = FindObjectOfType<Text>();
        }
        UpdateScoreUI();

        // Если не назначена область спавна, используем границы камеры
        if (spawnArea == null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                float screenWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
                float screenHeight = mainCamera.orthographicSize * 2;
                GameObject tempSpawnArea = new GameObject("SpawnArea");
                tempSpawnArea.transform.position = mainCamera.transform.position;
                spawnArea = tempSpawnArea.transform;
                // Можете визуально представить границы spawnArea (например, создав спрайт)
                Debug.LogWarning("Spawn Area not assigned. Using camera bounds.");
            }
            else
            {
                Debug.LogError("Main Camera not found.");
            }
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnTarget();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnTarget()
    {
        if (targetPrefab != null && spawnArea != null)
        {
            float randomX = Random.Range(spawnArea.position.x - 5f, spawnArea.position.x + 5f); // Пример диапазона
            float randomY = Random.Range(spawnArea.position.y - 3f, spawnArea.position.y + 3f); // Пример диапазона

            // Убедитесь, что цель не спавнится слишком близко к камере (если это важно)
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
            Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreTextPro != null)
        {
            scoreTextPro.text = "Score: " + score;
        }
        else if (scoreTextLegacy != null)
        {
            scoreTextLegacy.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("Score Text UI not found.");
        }
    }
}