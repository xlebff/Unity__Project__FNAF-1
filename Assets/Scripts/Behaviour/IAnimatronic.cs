using System.Collections;

public interface IAnimatronic
{
    IEnumerator Generating();
    void Moving();
    void OnCameraOpening();
    void Killed();
}
