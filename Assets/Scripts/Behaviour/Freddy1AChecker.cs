using UnityEngine;

public class Freddy1AChecker
{
    public static Freddy1AChecker Instance;

    public static bool Checked()
    {
        bool _bonnie = GameObject.FindGameObjectWithTag("Bonnie").GetComponent<BehaviourManager>().IsInCAM(CAM._1A);
        bool _chica = GameObject.FindGameObjectWithTag("Chica").GetComponent<BehaviourManager>().IsInCAM(CAM._1A);

        return !_bonnie && _chica;
    }
}
