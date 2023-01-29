using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines.Primitives;
using UnityEngine;

public class Emojie : MonoBehaviour
{
   [SerializeField] private Resizer _resizer;

  public Resizer Resizer => _resizer;

  private void OnDisable()
  {
      transform.localScale = Vector3.zero;
  }
}
