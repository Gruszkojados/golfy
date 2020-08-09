using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    public override void InitPlayer(Ball ball) {
        base.InitPlayer(ball);
        ball.SupscribeTouchCtl();
    }
}
