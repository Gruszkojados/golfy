using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPlayer : Player
{
    public override void StartTurn() {
        base.StartTurn();
        ball.Shoot(Random.Range(50,200), new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)));
    }
}

