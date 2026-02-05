using UnityEngine;

public class CloseDoor : DoorButton
{
    private delegate void SoundM();
    private SoundM _soundPlay;

    private Animator _anim;

    private void OnValidate() => _anim = _pressedState.GetComponent<Animator>();

    private void Awake()
    {
        SoundManager _sm = FindObjectOfType<SoundManager>();

        switch (_door)
        {
            case Door.Left:
                _soundPlay = _sm.LeftDoorSoundPlay;
                break;
            case Door.Right:
                _soundPlay = _sm.RightDoorSoundPlay;
                break;
        }

        _soundController = LightSoundController;
        _actionController = ActionController;
    }

    protected void LightSoundController() => _soundPlay();

    protected void ActionController() => _anim.Play(_isButtonActive ? "Close" : "Open");
}
