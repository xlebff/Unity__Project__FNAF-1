using System.Collections;
using UnityEngine;

public class SpritesChanger : MonoBehaviour
{
    [SerializeField] private GameObject[] _sprites;
    [SerializeField] private int _chance = 20;
    [SerializeField] private float _delay = 0.5f;

    private bool Show { get => Random.Range(0, _chance) == 1; }
    private int Index { get => Random.Range(0, _sprites.Length - 1); }

    private IEnumerator HideImage(GameObject obj)
    {
        yield return new WaitForSeconds(_delay);
        obj.SetActive(false);
    }

    private IEnumerator Checking()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);
            if (Show)
            {
                GameObject sprite = _sprites[Index];
                sprite.SetActive(true);
                StartCoroutine(HideImage(sprite));
            }
        }
    }

    private void Start() => StartCoroutine(Checking());
}
