using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CAM { _1A, _1B, _1C, _5, _3, _2A, _2B, _6, _7, _4A, _4B, DOOR };

public class CamerasManager : MonoBehaviour
{
    public static CamerasManager Instance;

    private static float _delay = 3f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public static void CamUpdate(CAM cam)
    {
        Debug.Log("Here");
        GameObject[] _allCams = GameObject.FindGameObjectsWithTag("Cam");

        foreach (GameObject _cam in _allCams)
        {
            OnEnableChecker tmp = _cam.GetComponent<OnEnableChecker>();
            if (tmp.GetCam() == cam)
            {
                tmp.isEnable = false;
                _cam.SetActive(false);
                Instance.StartCoroutine(Enabling(tmp));
            }
        }
    }

    private static IEnumerator Enabling(OnEnableChecker cam)
    {
        yield return new WaitForSeconds(_delay);
        cam.isEnable = true;
    }
}
