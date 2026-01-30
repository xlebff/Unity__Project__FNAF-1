using UnityEngine;
using UnityEngine.EventSystems;

public class MenuArrowFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private GameObject[] _arrows;
    [SerializeField] private AudioSource _audio;

    [SerializeField] private GameObject _current;

    private void Start() => _current = _buttons[0];

    void Update()
    {
        GameObject go = EventSystem.current?.currentSelectedGameObject;
        if (!go || (go == _current)) return;
        else _current = go;

        for (int i = 0; i < _buttons.Length; ++i)
        {
            if (go == _buttons[i])
            {
                foreach (GameObject arrow in _arrows)
                    arrow.gameObject.SetActive(false);

                _arrows[i].SetActive(true);
                _audio.Play();
            }
        }
    }

    public void PlaySound() => _audio.Play();
}
