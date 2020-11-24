using System;

public abstract class Player
{   
    public event Action OnTunrFinished = () => {};
    public Ball ball;
    public virtual void InitPlayer(Ball ball) {
        if(ball!=null) {
            ball.OnBallStoped -= OnBallStoped;
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
