using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action<Player> OnChangePlayer = (_) => {};
    public Ball ballPrefab;
    Player currentPlayer;
    Player[] players;

    Ball[] balls;

    private void Awake() {
        LvlController.OnLvlLoaded += OnLvlLoaded;
        Ball.OnAnyBallStop += OnBallStoped;
        InitPlayers();
        
    }
    private void OnDestroy() {
        LvlController.OnLvlLoaded -= OnLvlLoaded;
        Ball.OnAnyBallStop -= OnBallStoped;
    }

    void InitPlayers() {
        switch (PlayerProfile.gameMode)
        {
            case Gamemode.singlePlayer: 
                players = new Player[1];
                players[0] = new HumanPlayer();
                break;
            case Gamemode.bot:
                players = new Player[2];
                players[0] = new HumanPlayer();
                players[1] = new AiPlayer();
                break;
            default:
                break;
        }
        currentPlayer = players[0];
    }

    void OnLvlLoaded(Lvl _, int __) {
        if(balls!=null) {
            foreach (var item in balls)
            {
                Destroy(item.gameObject);
            }
        }
        balls = new Ball[players.Length];
        int index = 0;
        foreach (var item in players)
        {
            Ball ball = Instantiate(ballPrefab);
            ball.ActivateBall(false);
            item.InitPlayer(ball);
            balls[index] = ball;
            index++;
        }
        currentPlayer.StartTurn();
    }

    void OnBallStoped() {
        int index = 0;
        foreach (var item in players)
        {
            if(item == currentPlayer) {
                if(players.Length == (index+1)) {
                    currentPlayer = players[0];
                } else {
                    currentPlayer = players[index+1];
                }
                
                foreach (var oneBall in balls)
                {
                    oneBall.ActivateBall(false);
                }

                OnChangePlayer.Invoke(currentPlayer);
                currentPlayer.StartTurn();
                return;
            }
            index++;
        }
    }
}
