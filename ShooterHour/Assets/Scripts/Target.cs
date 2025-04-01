using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreValue = 10; // Количество очков за попадание

    private void OnMouseDown()
    {
        // Когда на цель нажимают мышью
        GameManager.instance.AddScore(scoreValue);
        Destroy(gameObject); // Уничтожаем цель
    }

    // Можно добавить логику появления цели здесь или в GameManager
}