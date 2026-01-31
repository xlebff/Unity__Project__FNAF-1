using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableChecker : MonoBehaviour
{
    [SerializeField] private CAM _cam;

    [SerializeField] private GameObject _allSprite;
    [SerializeField] private GameObject _bonnieSprite;
    [SerializeField] private GameObject _chicaSprite;
    [SerializeField] private GameObject _bonnieChicaSprite;
    [SerializeField] private GameObject _freddySprite;
    [SerializeField] private GameObject _freddyChicaSprite;
    [SerializeField] private GameObject _freddyBonnieSprite;
    [SerializeField] private GameObject _noneSprite;

    [SerializeField] private BehaviourManager _freddy;
    [SerializeField] private BehaviourManager _bonnie;
    [SerializeField] private BehaviourManager _chica;

    public bool isEnable = true;

    private void OnEnable()
    {
        DisableAll();

        if (!isEnable) return;

        bool freddy = _freddy != null && _freddy.IsInCAM(_cam);
        bool bonnie = _bonnie != null && _bonnie.IsInCAM(_cam);
        bool chica = _chica != null && _chica.IsInCAM(_cam);

        GameObject toEnable =
            (freddy && bonnie && chica) ? _allSprite :
            (freddy && bonnie) ? _freddyBonnieSprite :
            (freddy && chica) ? _freddyChicaSprite :
            (bonnie && chica) ? _bonnieChicaSprite :
            (freddy) ? _freddySprite :
            (bonnie) ? _bonnieSprite :
            (chica) ? _chicaSprite :
                                          _noneSprite;

        if (toEnable) toEnable.SetActive(true);
    }

    private void DisableAll()
    {
        if (_allSprite) _allSprite.SetActive(false);
        if (_bonnieSprite) _bonnieSprite.SetActive(false);
        if (_chicaSprite) _chicaSprite.SetActive(false);
        if (_bonnieChicaSprite) _bonnieChicaSprite.SetActive(false);
        if (_freddySprite) _freddySprite.SetActive(false);
        if (_freddyChicaSprite) _freddyChicaSprite.SetActive(false);
        if (_freddyBonnieSprite) _freddyBonnieSprite.SetActive(false);
        if (_noneSprite) _noneSprite.SetActive(false);
    }

    public CAM GetCam() => _cam;
}
