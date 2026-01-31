using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager : MonoBehaviour, TimeListener
{
    [SerializeField] private Enemies _enemy;
    [SerializeField] private float _delay = 3.02f;
    [SerializeField] private int _intelligence;
    [SerializeField] private CAM _currentCam = CAM._1A;
    [SerializeField] private bool[] _isChangingOnHour = new bool[3] { false, false, false };

    private Dictionary<CAM, CAM[]> _path;


    private void OnEnable()
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

    private void Start() => StartCoroutine(MovingChecker());

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
        if (_enemy == Enemies.Freddy && !Freddy1AChecker.Checked())
            return;
        Debug.Log("Moving");
        CamerasManager.CamUpdate(_currentCam);
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

    public CAM GetCurrentCAM() => _currentCam;

    public bool IsInCAM(CAM cam) => _currentCam == cam;
}
