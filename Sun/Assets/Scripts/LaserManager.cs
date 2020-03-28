using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LaserManager
{
    ObjectGrid _objectGrid;
    List<Vector3Int> _activeBlocks;
    HashSet<Vector3Int> _laserPath;

    GameObject _laserRenderer;
    LineRenderer _lineRenderer;
    public LaserManager(ObjectGrid grid)
    {
        _objectGrid = grid;
        _activeBlocks = new List<Vector3Int>();
        _laserPath = new HashSet<Vector3Int>();

        _laserRenderer = new GameObject("Laser Renderer");
    }

    public void FireLasers(Vector3Int startPos)
    {
        _activeBlocks.Clear();
        _laserPath.Clear();
        FireLasersFromBlock(startPos);
        UpdateLaserRender();

        foreach (var item in _laserPath) Debug.Log(item);
    }

    public void FireLasersFromBlock(Vector3Int pos)
    {
        _activeBlocks.Add(pos);
        ILaserInteractable block = _objectGrid.GetNodeAtPosition(pos).GetComponent<ILaserInteractable>();
        if (block == null) return;

        FaceUtils.Direction[] outputDirections = block.HandleLaserInput();
        FaceUtils.Face outputFace = block.GetOutputFace();
        foreach (var dir in outputDirections)
        {
            Vector3Int hitPos;
            block = CheckDirection(pos, dir, outputFace, out hitPos);
            if (block != null)
            {
                if (!_activeBlocks.Contains(hitPos))
                {
                    FireLasersFromBlock(hitPos);
                }
                else
                {
                    AddExtraInput(hitPos);
                }
            }
            else
            {
                _activeBlocks.Add(hitPos);
            }
        }
    }

    public void AddExtraInput(Vector3Int pos)
    {
        Debug.Log("adding input to active laser");
    }

    private ILaserInteractable CheckDirection(Vector3Int pos, FaceUtils.Direction dir, FaceUtils.Face f, out Vector3Int hitPos)
    {
        Vector3Int directionOfLaser = FaceUtils.GetDirectionFromFace(f, dir);

        bool horizontalStep;

        ILaserInteractable hit = null;

        GameObject o = _objectGrid.CheckDirection(pos, directionOfLaser, f, out hitPos, out horizontalStep, ref _laserPath);
        if (o != null) hit = o.GetComponent<ILaserInteractable>();

        if (hit != null)
        {
            if (IsHittingInputFace(f, hit.GetInputFace(), directionOfLaser, horizontalStep)) return hit;
        }

        return null;
    }
    private bool IsHittingInputFace(FaceUtils.Face output, FaceUtils.Face input, Vector3Int outputDir, bool horizontalStep)
    {

        if (output == input) return false;

        if (horizontalStep) //is hitting the left or right face
        {
            if ((outputDir.x > 0 && input == FaceUtils.Face.left) || (outputDir.x < 0 && input == FaceUtils.Face.right)) return true;
        }
        else // is hitting the top or bottom face
        {
            if ((outputDir.y > 0 && input == FaceUtils.Face.back) || (outputDir.y < 0 && input == FaceUtils.Face.front)) return true;
        }

        return false;
    }

    private void UpdateLaserRender()
    {
        if (_lineRenderer == null)
        {
            _lineRenderer = _laserRenderer.AddComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.2f;
        }

        _lineRenderer.positionCount = _activeBlocks.Count;

        Vector3[] points = new Vector3[_activeBlocks.Count];
        for (int i = 0; i < _activeBlocks.Count; i++)
        {
            points[i] = _activeBlocks[i];
        }

        _lineRenderer.SetPositions(points);
    }

    public void CheckActivePath(Vector3Int pos)
    {
        if (_activeBlocks.Contains(pos))
        {
            int i = _activeBlocks.IndexOf(pos);
            if (i == 0)
            {
                _activeBlocks.Clear();
                UpdateLaserRender();
            }
            else
            {
                _activeBlocks.RemoveRange(i, _activeBlocks.Count - i);
                FireLasersFromBlock(_activeBlocks[i - 1]);
                UpdateLaserRender();
            }
        }
    }

    public void CheckActivePath(Vector3Int pos, Vector3Int dir)
    {
        if (_laserPath.Contains(pos + dir) || _laserPath.Contains(pos))
        {
            Vector3Int startPos = _activeBlocks[0];
            _activeBlocks.Clear();
            FireLasers(startPos);
            UpdateLaserRender();

        }
    }
}
