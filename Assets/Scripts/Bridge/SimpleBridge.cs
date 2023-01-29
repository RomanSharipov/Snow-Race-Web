using UnityEngine;

public class SimpleBridge : IBridgeAction
{
    public void OnEnterStickman(Stickman stickman)
    {
        stickman.OnEnterOnBridge();
    }

    public void OnExitStickman(Stickman stickman)
    {
        stickman.OnExitFromBridge();
    }


}
