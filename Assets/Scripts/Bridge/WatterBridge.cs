using UnityEngine;

public class WatterBridge : IBridgeAction
{
    public void OnEnterStickman(Stickman stickman)
    {
        stickman.OnEnterOnWaterBridge();
    }

    public void OnExitStickman(Stickman stickman)
    {
        stickman.OnExitOnWaterBridge();
    }
}
