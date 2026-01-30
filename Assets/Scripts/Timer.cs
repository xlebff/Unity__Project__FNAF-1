using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private float _time = 0;
    [SerializeField] private int _secPerHour = 90; // 1 hour -> 90 sek
    [SerializeField] private TimeListener[] _listeners;

    public static Timer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private int _hour = 0;

    private void FixedUpdate()
    {
        _time += Time.deltaTime;

        if (_time >= _secPerHour)
        {
            _timer.text = (++_hour) + " PM";
            _time = 0;

            if (_listeners.Length > 0)
            {
                foreach (TimeListener listener in _listeners)
                    listener.OnHourChanged(_hour);
            }
        }

        if (_hour >= 6)
            Debug.Log("End of night");
    }

    public int GetTime() => _hour;
}
