using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public enum CAM { _1A, _1B, _1C, _5, _3, _2A, _2B, _6, _7, _4A, _4B, DOOR };

public class CameraManager : MonoBehaviour, IMovingListener
{
    [System.Serializable]
    public class Cameras
    {
        public CAM key;
        public GameObject value;
    }
    [System.Serializable]
    public class Texts
    {
        public CAM key;
        public string value;
    }

    [SerializeField] private List<Cameras> _camerasData = new();
    [SerializeField] private List<Cameras> _buttonsData = new();
    [SerializeField] private TMP_Text _text;
    [SerializeField] private List<Texts> _namesData = new();
    [SerializeField] private GameObject _stage;

    private Dictionary<CAM, GameObject> _cameras;
    private Dictionary<CAM, GameObject> _buttons;
    private Dictionary<CAM, string> _names;
    private Dictionary<CAM, bool> _enabledCameras = new()
    {
        { CAM._1A, true },
        { CAM._2A, true },
        { CAM._1B, true },
        { CAM._2B, true },
        { CAM._1C, true },
        { CAM._5, true },
        { CAM._6, true },
        { CAM._7, true },
        { CAM._3, true },
        { CAM._4A, true },
        { CAM._4B, true }
    };
    private const float DELAY = 3f;

    public static CameraManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        _cameras = _camerasData.ToDictionary(x => x.key, x => x.value);
        _buttons = _buttonsData.ToDictionary(x => x.key, x => x.value);
        _names = _namesData.ToDictionary(x => x.key, x => x.value);
    }

    public static CameraManager GetInstance() => Instance;

    public void CamUpdate(CAM _startCam, CAM _nextCam)
    {
        if (_nextCam == CAM.DOOR) _nextCam = _startCam;
        else if (_startCam == CAM.DOOR) _startCam = _nextCam;

        _enabledCameras[_startCam] = false;
        _enabledCameras[_nextCam] = false;

        _cameras[_startCam].SetActive(false);
        _cameras[_nextCam].SetActive(false);

        SoundManager.GetInstance().CameraErrorSound();

        StartCoroutine(Enabling(_startCam, _nextCam));
    }

    private IEnumerator Enabling(CAM _startCam, CAM _nextCam)
    {
        yield return new WaitForSeconds(DELAY);
        _enabledCameras[_startCam] = true;
        _enabledCameras[_nextCam] = true;
    }

    public void CameraChanging(CAM cam)
    {
        SoundManager.GetInstance().CameraChangeSoundPlay();

        foreach (var pair in _cameras)
        {
            if (pair.Key == cam && _enabledCameras[pair.Key])
            {
                pair.Value.SetActive(true);
                _buttons[pair.Key].SetActive(true);
                _text.text = _names[pair.Key];
            }
            else
            {
                pair.Value.SetActive(false);
                _buttons[pair.Key].SetActive(false);
            }
        }
    }

    public void MovingNotify()
    {
        _stage.SetActive(false);
    }
}
