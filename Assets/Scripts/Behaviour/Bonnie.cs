using System.Collections;
using UnityEngine;

public class Bonnie : Animatronic
{
    private float _timeToKill;
    private float _coef;
    private bool _isHunting = false, _isDetected = false;

    override protected void OnValidate()
    {
        base.OnValidate();
        _currentIntelligence = _intelligence[PlayerPrefs.GetInt("Night") - 1] - 3;
    }

    override public void OnHourChanged(int hour)
    {
        switch (hour)
        {
            case 2:
            case 3:
            case 4:
                ++_currentIntelligence;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (_isHunting)
        {
            bool isDoorClose = _door.IsButtonActive;
            bool isDoorLighted = _light.IsButtonActive;

            _coef = isDoorClose ? 1 : 1.3f;
            _timeToKill -= Time.deltaTime * _coef;

            if (isDoorLighted && !_isDetected)
            {
                _isDetected = true;
                SoundManager.GetInstance().LeftDoorNotifySoundPlay();
            }

            if (_timeToKill < 0)
            {
                if (isDoorClose)
                {
                    Moving();
                    _isHunting = false;
                    _isDetected = false;
                }
                else
                    Killed();
            }
        }
    }

    public override void Moving()
    {
        base.Moving();

        if (_currentCam == CAM.DOOR)
        {
            _timeToKill = Random.Range(_minKillingDelay, _maxKillingDelay);
            _isHunting = true;
        }
    }
}