using UnityEngine;

public class Rotation : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _sensitivity = 0.1f;
    [SerializeField] private float _maxPosition = 95f;
    [SerializeField] private float _minPosition = -95f;

    private RectTransform _rectTransform;
    private Vector2 _officePos;
    private float _screenCenterX;
    private float _currentX;

    private void OnValidate()
    {
        _rectTransform = GetComponent<RectTransform>();

        if (_rectTransform == null)
        {
            Debug.LogError("OfficeMovement requires RectTransform component!");
            enabled = false;
            return;
        }
    }

    private void Awake()
    {
        // Получаем начальную позицию
        _officePos = _rectTransform.anchoredPosition;
        _currentX = _officePos.x;

        // Центр экрана
        _screenCenterX = Screen.width * 0.5f;
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        // Вычисляем отклонение от центра экрана
        // Нормализуем от -1 до 1
        float normalizedOffset = (mousePos.x - _screenCenterX) / _screenCenterX;

        // Умножаем на чувствительность и максимальное смещение
        float targetX = normalizedOffset * _maxPosition * _sensitivity;

        // Плавная интерполяция к целевой позиции
        _currentX = Mathf.Lerp(_currentX, targetX, Time.deltaTime * 5f);

        // Ограничиваем диапазон
        _currentX = Mathf.Clamp(_currentX, _minPosition, _maxPosition);

        // Обновляем позицию
        _officePos.x = -_currentX;
        _rectTransform.anchoredPosition = _officePos;
    }
}