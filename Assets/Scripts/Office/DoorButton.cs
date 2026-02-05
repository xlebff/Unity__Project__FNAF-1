using UnityEngine;
using UnityEngine.UI;

public class DoorButton : MonoBehaviour
{
    protected enum Door { Left, Right };

    [Header("Current Door")]
    [SerializeField] protected Door _door;
    [SerializeField] protected GameObject _pressedState;

    [Header("Button")]
    [SerializeField] private GameObject _button;

    [Header("Button Sprites")]
    [SerializeField] private Sprite _unpressedButton;
    [SerializeField] private Sprite _pressedButton;

    protected bool _isButtonActive = false;
    public bool IsButtonActive { get => _isButtonActive; }

    protected delegate void Action();
    protected Action _soundController, _actionController;

    public void ChangeState()
    {
        _isButtonActive = !_isButtonActive;
        _button.GetComponent<Image>().sprite = _isButtonActive ? _pressedButton : _unpressedButton;

        _actionController();
        _soundController();
    }
    public void PowerDownNotify()
    {
        _isButtonActive = false;
        _pressedState.SetActive(_isButtonActive);
    }
}
