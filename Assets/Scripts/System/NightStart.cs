using System.Collections;
using TMPro;
using UnityEngine;

public class NightStart : MonoBehaviour
{
    [SerializeField] private GameObject _transition;
    [SerializeField] private GameObject[] _toEnable;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _delay = 5f;

    private void OnValidate() => _text = GetComponent<TMP_Text>();

    private void Start()
    {
        _text.text = "Night " + PlayerPrefs.GetInt("Night")
            + "\n12 AM";

        StartCoroutine(Enabler());
    }

    private IEnumerator Enabler()
    {
        yield return new WaitForSeconds(_delay);

        _transition.SetActive(true);

        foreach (GameObject obj in _toEnable)
            obj.SetActive(true);

        this.gameObject.SetActive(false);
    }
}
