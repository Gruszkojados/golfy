using System;

public class HumanPlayer : Player
{
    public static event Action OnHumanStartTurn = () => { };
    public override void InitPlayer(Ball ball) {
        base.InitPlayer(ball);
        ball.SupscribeTouchCtl(); // Subscribe touch input. Using only by HumanPlayer.
    }

    public override void StartTurn() {
        base.StartTurn();
        base.ball.RotationBarDisplay(true); // Enable ball rotation.
        OnHumanStartTurn.Invoke();
    }
}
