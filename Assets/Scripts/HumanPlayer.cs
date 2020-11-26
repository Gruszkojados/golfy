using System;

public class HumanPlayer : Player
{
    public static event Action OnHumanStartTurn = () => { };
    public override void InitPlayer(Ball ball) {
        base.InitPlayer(ball);
        ball.SupscribeTouchCtl(); // subscribe touch input, only Human Player !
    }

    public override void StartTurn() {
        base.StartTurn();
        base.ball.RotationBarDisplay(true); // enable ball rotation
        OnHumanStartTurn.Invoke();
    }
}
