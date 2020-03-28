
using UnityEngine;

public interface ILaserInteractable
{
    FaceUtils.Direction[] HandleLaserInput();
    FaceUtils.Face GetInputFace();
    FaceUtils.Face GetOutputFace();
    void SetOutputFace(FaceUtils.Face f);
    void SetInputFace(FaceUtils.Face f);
}
