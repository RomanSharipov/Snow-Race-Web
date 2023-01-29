using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBridgeAction 
{
    public void OnEnterStickman(Stickman stickman);
    public void OnExitStickman(Stickman stickman);
}
