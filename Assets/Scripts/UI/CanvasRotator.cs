using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines.Primitives;
using UnityEngine;

public class CanvasRotator : MonoBehaviour
{
  private Transform _camera;
    
  private void Start()
  {
    _camera = Camera.main.transform;
  }

  private void Update()
  {
    transform.forward = _camera.forward * -1;
  }
}
