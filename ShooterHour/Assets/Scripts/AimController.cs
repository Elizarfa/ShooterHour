using UnityEngine;

public class AimController : MonoBehaviour
{
    void Update()
    {
        // Получаем позицию мыши в мировых координатах
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Устанавливаем Z-координату камеры
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Перемещаем прицел к позиции мыши
        transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
    }
}
