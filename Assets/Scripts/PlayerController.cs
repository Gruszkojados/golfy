using System;
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
        //Debug.Log("Player kontroller jest inicjowany");
        LvlController.OnLvlLoaded += OnLvlLoaded;
        Ball.OnAnyBallStop += OnBallStoped;
        InitPlayers();
    }
    private void OnDestroy() {
        LvlController.OnLvlLoaded -= OnLvlLoaded;
        Ball.OnAnyBallStop -= OnBallStoped;
    }

    void InitPlayers() {
        //Debug.Log("INICJUJE GRACZY !");
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

    void OnLvlLoaded(Lvl _, int __) {
        //Debug.Log("PC ON LVL LOADED");
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
            ball.RotationBarDisplay(false);
            balls[index] = ball;
            index++;
        }
        if(players.Length>1) {
            players[1].ball.transform.position = new Vector3(4f,0f,0f);
        }
        currentPlayer = players[0];
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