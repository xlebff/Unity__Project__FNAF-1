using UnityEngine;

public class CameraManagerHelper : MonoBehaviour
{
    [SerializeField] private CAM cam;

    public void CameraChanging() => CameraManager.GetInstance().CameraChanging(cam);
}
