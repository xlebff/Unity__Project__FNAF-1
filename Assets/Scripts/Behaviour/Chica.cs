using UnityEngine;

public class Chica : Animatronic
{
    override protected void OnValidate()
    {
        base.OnValidate();
        _currentIntelligence = _intelligence[PlayerPrefs.GetInt("Night") - 1] - 2;
    }

    override public void OnHourChanged(int hour)
    {
        switch (hour)
        {
            case 3:
            case 4:
                ++_currentIntelligence;
                break;
        }
    }
}
