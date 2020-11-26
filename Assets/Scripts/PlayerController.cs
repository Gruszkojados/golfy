﻿using System;
using System.Collections;
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

    void InitPlayers() { // init all player in case of game type
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
    }

    void OnLvlLoaded(Lvl _, int __) { // function for load levels
        if(balls!=null) {
            foreach (var item in balls) // destroy whole old balls
            {
                Destroy(item.gameObject);
            }
        }
        balls = new Ball[players.Length];
        int index = 0;
        foreach (var player in players) // init all players
        {   
            Ball ball = Instantiate(ballPrefab);
            ball.ActivateBall(false);
            player.InitPlayer(ball); // init ball for player
            ball.RotationBarDisplay(false);
            if(player.GetType() == typeof(AiPlayer)) {
                ball.setOwner(true); // seting ball owner
            }
            balls[index] = ball;
            index++;
        }
        if(players.Length > 1) { // make some space between players
            players[0].ball.transform.position = new Vector3(-2f,0f,0f);
            players[1].ball.transform.position = new Vector3(2f,0f,0f);
        }
        currentPlayer = players[0];
        OnChangePlayer.Invoke(currentPlayer);
        currentPlayer.StartTurn();
    }

    void OnBallStoped() { // function for end of player turn
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
                SoundsAction.ChangePlayer();
                OnChangePlayer.Invoke(currentPlayer);

                if(currentPlayer.GetType() == typeof(AiPlayer)) {
                    StartCoroutine(BotWait(currentPlayer));
                } else {
                    currentPlayer.StartTurn();
                }
                return;
            }
            index++;
        }
    }
    public IEnumerator BotWait(Player player) { // bot player shoot delay
        yield return new WaitForSeconds(1f);
        player.StartTurn();
    }
}