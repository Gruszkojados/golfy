using System;

public abstract class Player // Common player class.
{   
    public event Action OnTunrFinished = () => {};
    public Ball ball;
    public TargetBlock targetBlock;
    public virtual void InitPlayer(Ball ball) {
        InitPlayer(ball, null);
    }

    public virtual void InitPlayer(Ball ball, TargetBlock tb) {
        if(ball!=null) {
            ball.OnBallStoped -= OnBallStoped;
        }

        if(tb!=null) {
            this.targetBlock = tb;
        }

        this.ball = ball;
        ball.OnBallStoped += OnBallStoped;
    }
    

    private void OnDestroy() {
        ball.OnBallStoped -= OnBallStoped;
    }

    void OnBallStoped(Ball ball) {
        OnTunrFinished.Invoke();
    }

    public virtual void StartTurn() {
        ball.ActivateBall(true);
    }
    
}
