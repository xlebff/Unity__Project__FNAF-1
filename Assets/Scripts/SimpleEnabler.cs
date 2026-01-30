using UnityEngine;

public class SimpleEnabler : MonoBehaviour
{
    [SerializeField] private GameObject enabler;
    [SerializeField] private GameObject[] disable;

    public void ObjEnable() => enabler.SetActive(true);

    public void ObjDisable() => enabler.SetActive(false);

    public void ExpObjEnable()
    {
        foreach (GameObject obj in disable)
            obj.SetActive(false);

        enabler.SetActive(true);
    }
}
