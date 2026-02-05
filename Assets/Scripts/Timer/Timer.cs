using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private float _time = 0;
    [SerializeField] private int _secPerHour = 90; // 1 hour -> 90 sek
    [SerializeField] private GameObject[] _listenersObjects;

    private ITimeListener[] _listeners;

    public static Timer Instance { get; private set; }

    private void OnValidate()
    {
        _listeners = new ITimeListener[_listenersObjects.Length];

        for (int i = 0; i < _listenersObjects.Length; i++)
            _listeners[i] = _listenersObjects[i].GetComponent<ITimeListener>();
    }

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
                foreach (ITimeListener listener in _listeners)
                    listener.OnHourChanged(_hour);
            }
        }

        if (_hour >= 6)
            Debug.Log("End of night");
    }

    public int GetTime() => _hour;
}
