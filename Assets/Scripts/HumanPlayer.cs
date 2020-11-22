using System;

public class HumanPlayer : Player
{
    public static event Action OnHumanStartTurn = () => { };
    public override void InitPlayer(Ball ball) {
        base.InitPlayer(ball);
        ball.SupscribeTouchCtl();
    }

    public override void StartTurn() {
        base.StartTurn();
        base.ball.RotationBarDisplay(true);
        OnHumanStartTurn.Invoke();
    }
}
