using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraOpen : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject _camera;
    private bool _isOpen = false;
    private bool _isEnable = true;
    private const float _delay = 0.5f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered camera");

        if (_isEnable)
        {
            _camera.SetActive(true);
            _camera.GetComponent<Animator>().Play(_isOpen ? "Close" : "Open");

            if (!_isOpen) SoundManager.GetInstance().CameraSoundEnable();

            SoundManager.GetInstance().CameraOpenSoundPlay();

            if (_isOpen) SoundManager.GetInstance().CameraSoundDisable();

            _isOpen = !_isOpen;
            _isEnable = false;

            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);
        _isEnable = true;
    }
}
