using UnityEngine;

public class SimpleEnabler : MonoBehaviour
{
    [SerializeField] private GameObject[] enabler;
    [SerializeField] private GameObject[] disable;

    public void ExpObjEnable()
    {
        foreach (GameObject obj in enabler)
            obj.SetActive(true);

        foreach (GameObject obj in disable)
            obj.SetActive(false);
    }

    public void ExpObjDisable()
    {
        foreach (GameObject obj in enabler)
            obj.SetActive(false);

        foreach (GameObject obj in disable)
            obj.SetActive(true);
    }

    public void ObjEnable()
    {
        foreach (GameObject obj in enabler)
            obj.SetActive(true);
    }

    public void ObjDisable()
    {
        foreach (GameObject obj in disable)
            obj.SetActive(false);
    }
}
