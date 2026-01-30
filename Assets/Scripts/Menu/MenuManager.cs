using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _transition;
    [SerializeField] private float _delay = 0.2f;

    public void NewGame()
    {
        PlayerPrefs.SetInt("Started", 1);
        PlayerPrefs.SetInt("Night", 1);
        StartCoroutine(Transition("Start"));
    }

    public void Continue() {
        if (PlayerPrefs.GetInt("Started") == 0)
            StartCoroutine(Transition("Start"));
        else
            StartCoroutine(Transition("Night")); 
    }

    private IEnumerator Transition(string scene)
    {
        _transition.SetActive(true);
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(scene);
    }
}
