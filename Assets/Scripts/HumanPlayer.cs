using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    public override void InitPlayer(Ball ball) {
        base.InitPlayer(ball);
        //ball.RotationBarDisplay(true);
        ball.SupscribeTouchCtl();
    }

    public override void StartTurn() {
        base.StartTurn();
        base.ball.RotationBarDisplay(true);
    }
}
