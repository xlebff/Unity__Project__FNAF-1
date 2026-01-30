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

    private BehaviourManager _freddy;
    private BehaviourManager _bonnie;
    private BehaviourManager _chica;

    private void OnValidate()
    {
        _freddy = GameObject.FindGameObjectWithTag("Freddy").GetComponent<BehaviourManager>();
        _bonnie = GameObject.FindGameObjectWithTag("Bonnie").GetComponent<BehaviourManager>();
        _chica = GameObject.FindGameObjectWithTag("Chica").GetComponent<BehaviourManager>();
    }

    private void OnEnable()
    {
        if (_allSprite) _allSprite.SetActive(false);
        if (_bonnieSprite) _bonnieSprite.SetActive(false);
        if (_chicaSprite) _chicaSprite.SetActive(false);
        if (_bonnieChicaSprite) _bonnieChicaSprite.SetActive(false);
        if (_freddySprite) _freddySprite.SetActive(false);
        if (_freddyChicaSprite) _freddyChicaSprite.SetActive(false);
        if (_freddyBonnieSprite) _freddyBonnieSprite.SetActive(false);
        if (_noneSprite) _noneSprite.SetActive(false);

        bool freddy = _freddy.IsInCAM(_cam);
        bool bonnie = _bonnie.IsInCAM(_cam);
        bool chica = _chica.IsInCAM(_cam);

        if (freddy && bonnie && chica) _allSprite.SetActive(true);
        else if (freddy && bonnie) _freddyBonnieSprite.SetActive(true);
        else if (freddy && chica) _freddyChicaSprite.SetActive(true);
        else if (freddy) _freddySprite.SetActive(true);
        else if (bonnie && chica) _bonnieChicaSprite.SetActive(true);
        else if (bonnie) _bonnieSprite.SetActive(true);
        else if (chica) _chicaSprite.SetActive(true);
        else _noneSprite.SetActive(true);
    }
}
