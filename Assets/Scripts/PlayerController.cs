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
        Debug.Log("Player kontroller jest inicjowany");
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
        foreach (var player in players)
        {
            Ball ball = Instantiate(ballPrefab);
            ball.ActivateBall(false);
            player.InitPlayer(ball);
            balls[index] = ball;
            index++;
        }
        if(currentPlayer.GetType().Equals("AiPlayer")){
            currentPlayer = players[0];
            
        }
        Debug.Log("OnLvlLoaded player type: " + currentPlayer.GetType().ToString());
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

                if(currentPlayer.GetType() == typeof(AiPlayer)) {
                    StartCoroutine(botWait(currentPlayer));
                   
                } else {
                    currentPlayer.StartTurn();
                }
                return;
            }
            index++;
        }
    }
    public IEnumerator botWait(Player player) {
        yield return new WaitForSeconds(1);
        player.StartTurn();
        Debug.Log("Bot start turn");
    }
}
