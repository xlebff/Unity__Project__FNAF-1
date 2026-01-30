using System.Collections;
using UnityEngine;

public class Impulse : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    [Header("Fade Duration")]
    [SerializeField] private float _minFadeDuration = 0.13f;
    [SerializeField] private float _maxFadeDuration = 3f;

    [Header("Alpha Settings")]
    [SerializeField] private float _minStartAlpha = 0.5f;
    [SerializeField] private float _maxStartAlpha = 1f;

    [Header("Delay")]
    [SerializeField] private float _delay = 5f;
    [SerializeField] private int _interruptRate = 5;

    private readonly float FADE_RATE = 0.15f;

    private float StartAlpha { get => Random.Range(_minStartAlpha, _maxStartAlpha) / 255f; }
    private float FadeDuration { get => Random.Range(_minFadeDuration, _maxFadeDuration); }
    private float Delay { get => Random.Range(0f, 1f) * _delay; }
    private bool Interrupt { get => Random.Range(0, _interruptRate) == 1; }

    private IEnumerator HideImage()
    {
        Color tmp = _sprite.color;

        while (true)
        {
            float fadeDuration = FadeDuration;
            float startAlpha = StartAlpha;

            float dif = startAlpha / (fadeDuration / FADE_RATE);

            tmp.a = startAlpha;
            _sprite.color = tmp;

            for (float i = 0f; i < fadeDuration; i += FADE_RATE)
            {
                tmp.a -= dif;
                tmp.a = Mathf.Clamp01(tmp.a);
                _sprite.color = tmp;

                if (Interrupt)
                {
                    tmp.a = startAlpha;
                    _sprite.color = tmp;
                    i = 0f;
                }

                yield return new WaitForSeconds(FADE_RATE);
            }

            yield return new WaitForSeconds(Delay);
        }
    }

    private void Start() => StartCoroutine(HideImage());
}