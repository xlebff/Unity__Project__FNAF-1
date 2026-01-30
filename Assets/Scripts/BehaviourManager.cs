using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CAM { _1A, _1B, _1C, _5, _3, _2A, _2B, _6, _7, _4A, _4B, DOOR };

public class BehaviourManager : MonoBehaviour, TimeListener
{
    [SerializeField] private Enemies _enemy;
    [SerializeField] private float _delay = 3.02f;
    [SerializeField] private int _intelligence;
    [SerializeField] private bool[] _isChangingOnHour = new bool[4] { false, false, false, false };
    private Dictionary<CAM, CAM[]> _path;
    [SerializeField] private CAM _currentCam = CAM._1A;
    
    private void OnValidate()
    {
        _intelligence = Intelligence.GetIntelligence(_enemy);
        _path = Intelligence.GetPath(_enemy);

        switch (_enemy)
        {
            case (Enemies.Bonnie):
                _intelligence -= 3;
                for (int i = 0; i < _isChangingOnHour.Length; ++i)
                    _isChangingOnHour[i] = true;
                break;
            case (Enemies.Foxy):
            case (Enemies.Chica):
                _intelligence -= 2;
                for (int i = 1; i < _isChangingOnHour.Length; ++i)
                    _isChangingOnHour[i] = true;
                break;
            default:
                break;
        }
    }

    private IEnumerator MovingChecker()
    {
        while (true) {
            yield return new WaitForSeconds(_delay);
            if (Random.Range(1, 21) <= _intelligence)
                Moving();
        }
    }

    private void Moving()
    {
        _currentCam = _path[_currentCam][Random.Range(0, _path[_currentCam].Length)];
    }

    public void OnHourChanged(int _hour)
    {
        switch (_hour)
        {
            case 2:
                ChangingChecker(0);
                break;
            case 3:
                ChangingChecker(1);
                break;
            case 4:
                ChangingChecker(2);
                break;
            default:
                break;
        }
    }

    private void ChangingChecker(int index)
    {
        if (_isChangingOnHour[index])
            ++_intelligence;
    }
}
