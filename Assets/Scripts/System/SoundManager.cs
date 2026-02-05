using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private AudioSource _cameraSound;
    [SerializeField] private AudioSource _monitorSound;
    [SerializeField] private AudioClip _cameraChange;
    [SerializeField] private AudioClip[] _cameraGarbles;
    [SerializeField] private int _garblesChance = 10;

    [Header("Light")]
    [SerializeField] private AudioSource _lightSoundLeft;
    [SerializeField] private AudioSource _lightSoundRight;

    [Header("Door")]
    [SerializeField] private AudioSource _doorSoundLeft;
    [SerializeField] private AudioSource _doorSoundRight;

    [Header("Door Notify")]
    [SerializeField] private AudioSource _notifySoundLeft;
    [SerializeField] private AudioSource _notifySoundRight;

    private static SoundManager Instance;

    public static SoundManager GetInstance() => Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void CameraChangeSoundPlay()
    {
        _cameraSound.clip = _cameraChange;
        _cameraSound.Play();
    }

    public void CameraErrorSound()
    {
        if (Random.Range(1, 21) <= _garblesChance)
        {
            _cameraSound.clip = _cameraGarbles[Random.Range(0, _cameraGarbles.Length)];
            _cameraSound.Play();
        }
    }

    public void CameraOpenSoundPlay() => _monitorSound.Play();
    public void CameraSoundDisable() => _cameraSound.enabled = false;
    public void CameraSoundEnable() => _cameraSound.enabled = true;
    public void LeftLightSoundPlay() => _lightSoundLeft.enabled = true;
    public void RightLightSoundPlay() => _lightSoundRight.enabled = true;
    public void LeftDoorSoundPlay() => _doorSoundLeft.Play();
    public void RightDoorSoundPlay() => _doorSoundRight.Play();
    public void LeftLightSoundStop() => _lightSoundLeft.enabled = false;
    public void RightLightSoundStop() => _lightSoundRight.enabled = false;
    public void LeftDoorNotifySoundPlay() => _notifySoundLeft.Play();
    public void RightDoorNotifySoundPlay() => _notifySoundRight.Play();
}
