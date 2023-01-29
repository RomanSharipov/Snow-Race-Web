using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTrail : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _offsetOnGround;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _delayBetweenDrawing;
    [SerializeField] private Transform _snowBallRotation;
    [SerializeField] private int  _sizeTrail;
    [SerializeField] private float  _speedMoveDown;

    private float _lastDrawingTime;
    private Vector3 _drawingPointPosition = new Vector3();
    private Vector3 _newPosition;

    private void Update()
    {
        SetTrailPosition();
        MoveDownTail();
    }

    private void SetTrailPosition()
    {
        Ray ray = new Ray(transform.position, -Vector3.up * 100);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit,100, _groundLayerMask))
        {
            _drawingPointPosition = new Vector3(_snowBallRotation.transform.position.x, hit.point.y + _offsetOnGround, _snowBallRotation.transform.position.z);

            _trailRenderer.gameObject.transform.position = _drawingPointPosition;
            _lastDrawingTime = _delayBetweenDrawing;
        }
    }

    public void SwithOffTrail()
    {
        _trailRenderer.emitting = false;
    }

    public void SwithOnTrail()
    {
        _trailRenderer.emitting = true;
    }

    private void MoveDownTail()
    {
        _sizeTrail = _trailRenderer.positionCount;

        for (int i = 0; i < _sizeTrail; i++)
        {
            _newPosition = _trailRenderer.GetPosition(i) - new Vector3(0, _speedMoveDown * Time.deltaTime, 0);

            _trailRenderer.SetPosition(i, _newPosition);
        }
    }
}
