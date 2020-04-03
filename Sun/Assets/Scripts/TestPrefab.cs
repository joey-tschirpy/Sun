using System.Collections.Generic;
using UnityEngine;

public class TestPrefab : MonoBehaviour, ILaserInteractable
{
    [SerializeField] private FaceUtils.Face _inputFace = FaceUtils.Face.back;
    [SerializeField] private FaceUtils.Face _outputFace = FaceUtils.Face.front;
    [SerializeField] private FaceUtils.Direction[] _directions = { FaceUtils.Direction.straight };

    [SerializeField] private bool debugMode = true;


    public FaceUtils.Direction[] HandleLaserInput() => _directions;

    public FaceUtils.Face GetInputFace() => _inputFace;

    public FaceUtils.Face GetOutputFace() => _outputFace;


    public void SetOutputFace(FaceUtils.Face f) => _outputFace = f;

    public void SetInputFace(FaceUtils.Face f) => _inputFace = f;

    private void Update()
    {
        if (debugMode)
        {
            DebugInputDirection();
            DebugOutputDirection();
        }
    }

    private void DebugInputDirection()
    {
        Debug.DrawRay(transform.position, FaceUtils.GetDirectionFromFace(_inputFace), Color.blue);
    }

    private void DebugOutputDirection()
    {
        foreach (FaceUtils.Direction dir in _directions)
        {
            Debug.DrawRay(transform.position, FaceUtils.GetDirectionFromFace(_outputFace, dir), Color.green);
        }
    }
}
