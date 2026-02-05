public class LightDoor : DoorButton
{
    private delegate void SoundM();
    private SoundM _soundPlay, _soundStop;

    private void Awake()
    {
        SoundManager _sm = SoundManager.GetInstance();

        switch (_door)
        {
            case Door.Left:
                _soundPlay = _sm.LeftLightSoundPlay;
                _soundStop = _sm.LeftLightSoundStop;
                break;
            case Door.Right:
                _soundPlay = _sm.RightLightSoundPlay;
                _soundStop = _sm.RightLightSoundStop;
                break;
        }

        _soundController = LightSoundController;
        _actionController = ActionController;
    }

    protected void LightSoundController()
    {
        if (!_isButtonActive) _soundStop();
        else _soundPlay();
    }

    protected void ActionController() =>
        _pressedState.SetActive(_isButtonActive);
}
