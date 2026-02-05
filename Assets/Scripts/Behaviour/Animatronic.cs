using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Animatronic : MonoBehaviour, IAnimatronic, ITimeListener
{
    [System.Serializable]
    public class PathData
    {
        public CAM key;
        public CAM[] value;
    }

    [System.Serializable]
    public class SpritesData
    {
        public CAM key;
        public GameObject[] value;
    }

    [System.Serializable]
    public class SecretData
    {
        public CAM key;
        public GameObject value;
    }

    [Header("Cameras and sprites")]
    [SerializeField] private List<PathData> _pathData = new();
    [SerializeField] private List<SpritesData> _spritesData = new();
    [SerializeField] private List<SecretData> _secretData = new();

    [SerializeField] protected CAM _currentCam = CAM._1A;
    [SerializeField] protected Animator _anim;

    [Header("Behaviour")]
    [SerializeField] protected CloseDoor _door;
    [SerializeField] protected LightDoor _light;
    [SerializeField] protected int[] _intelligence;
    [SerializeField] protected int _currentIntelligence;
    [SerializeField] protected float _delay;
    [SerializeField] protected float _minKillingDelay;
    [SerializeField] protected float _maxKillingDelay;
    [SerializeField] protected bool _isActive = true;

    [Header("Listeners")]
    [SerializeField] private GameObject[] _listenersObjects;

    protected Dictionary<CAM, CAM[]> _path;
    protected Dictionary<CAM, GameObject[]> _sprites;
    protected Dictionary<CAM, GameObject> _secret;

    protected IMovingListener[] _listeners;

    private readonly int _easterEgg = 10;

    virtual protected void OnValidate()
    {
        _anim = _anim != null ? _anim : GetComponent<Animator>();

        _listeners = new IMovingListener[_listenersObjects.Length];

        for (int i = 0; i < _listenersObjects.Length; i++)
            _listeners[i] = _listenersObjects[i].GetComponent<IMovingListener>();
    }

    private void Awake()
    {
        _path = _pathData.ToDictionary(x => x.key, x => x.value);
        _sprites = _spritesData.ToDictionary(x => x.key, x => x.value);
        _secret = _secretData.ToDictionary(x => x.key, x => x.value);

#if UNITY_EDITOR
        if (_path.Count == 0 ||
            _sprites.Count == 0)
            Debug.LogError(name + ": Error during filling the dictionaties");
#endif
    }

    abstract public void OnHourChanged(int hour);

    virtual protected void Start() => StartCoroutine(Generating());

    virtual public IEnumerator Generating()
    {
        while (_isActive)
        {
            yield return new WaitForSeconds(_delay);

            if (Random.Range(1, 21) <= _currentIntelligence)
                Moving();
#if UNITY_EDITOR
            else Debug.Log(name + ": Not moving");
#endif
        }
    }

    virtual public void Moving()
    {
        CAM _nextCam = _path[_currentCam][Random.Range(0, _path[_currentCam].Length)];
        CameraManager.GetInstance().CamUpdate(_currentCam, _nextCam);

        Debug.Log(name + ": Moving from " + _currentCam + " to " + _nextCam);

        foreach (GameObject obj in _sprites[_currentCam])
            obj.SetActive(false);

        _sprites[_nextCam][Random.Range(0, _sprites[_nextCam].Length)].SetActive(true);

        _currentCam = _nextCam;

        if (_listeners.Length > 0)
        {
            foreach (IMovingListener listener in _listeners)
                listener.MovingNotify();
        }
    }

    virtual public void OnCameraOpening()
    {
        if (Random.Range(1, 21) <= _easterEgg)
        {
            Debug.Log(name + ": Trying to activate easter egg");

            if (_secret.ContainsKey(_currentCam))
            {
                foreach (GameObject obj in _sprites[_currentCam])
                    obj.SetActive(false);

                _secret[_currentCam].SetActive(true);

                Debug.Log(name + ": Easter egg activated");
            }
#if UNITY_EDITOR
            else Debug.Log(name + ": No easter egg on current camera");
#endif
        }
    }

    public void Killed()
    {
        _anim.enabled = true;
        Debug.Log(name + ": Killing");
    }
}