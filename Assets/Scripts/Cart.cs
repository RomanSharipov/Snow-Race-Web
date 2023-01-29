using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SplineFollower))]
public class Cart : MonoBehaviour
{
    [SerializeField] private int _cartPushSpeed;
    [SerializeField] private int _mediumSppeed;
    [SerializeField] private int _fastSpeed;
    [SerializeField] private GameObject _cartMesh;
    [SerializeField] private Transform _cartMeshSecondPosition;
 
    private const float AcelerationSpeed=1.2f;
    
    private const float CartPushDuration=0.7f;
    
    private SplineFollower _splineFollower;

    private void Awake()
    {
        _splineFollower = GetComponent<SplineFollower>();
    }

    public void StartFolow()
    {
        _splineFollower.follow = true;
    }

    public void Accelerate()
    {
        if (_splineFollower.followSpeed <_mediumSppeed)
        {
            StartCoroutine(SmoothlyAccelerate(_mediumSppeed));
        }
        else
        {
            StartCoroutine(SmoothlyAccelerate(_fastSpeed));
           // _splineFollower.followSpeed = _fastSpeed;
        }
        
    }

    private IEnumerator SmoothlyAccelerate(float targetSpeed)
    {
        while (_splineFollower.followSpeed<targetSpeed)
        {
            _splineFollower.followSpeed += AcelerationSpeed;
            yield return null;
        }
    }
    
    public void PushBackMesh()
    {
        _cartMesh.transform.parent = transform.parent;
        //StartCoroutine(Push());
         _cartMesh.transform.DOJump(_cartMeshSecondPosition.position,5 ,1,CartPushDuration);
    }

    private IEnumerator Push()
    {
        while (_cartMesh.transform.position!=_cartMeshSecondPosition.position)
        {
            _cartMesh.transform.position = Vector3.MoveTowards(_cartMesh.transform.position,
                _cartMeshSecondPosition.position, _cartPushSpeed);
            Debug.Log("выполняется");
            yield return null;
        }
    }
}
