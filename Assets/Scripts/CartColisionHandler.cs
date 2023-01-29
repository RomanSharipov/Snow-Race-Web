using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class CartColisionHandler : MonoBehaviour
{
    [SerializeField] private Transform _cartPosition;
    [SerializeField] private Cart _cart;
   [SerializeField] private SplineComputer _splineComputer;

   private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Stickman>( out Stickman stickman))
        {
            stickman.transform.parent = _cart.transform;
            stickman.JumpTuCart(_cartPosition,_splineComputer,_cart);
        }
    }
}
