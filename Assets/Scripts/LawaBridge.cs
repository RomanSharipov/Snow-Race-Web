using UnityEngine;

public class LawaBridge : IBridgeAction
{
    public void OnEnterStickman(Stickman stickman)
    {
        stickman.JumpOnSnowBall();
    }

    public void OnExitStickman(Stickman stickman)
    {
        stickman.JumpOffTheBall();
    }

}
