using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player
{   
    public event Action OnTunrFinished = () => {};
    protected Ball ball;
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
        // ball.ActivateBall(false);
    }

    public virtual void StartTurn() {
        ball.ActivateBall(true);
    }
}
