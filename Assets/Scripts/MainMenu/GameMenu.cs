using System;
using UnityEngine;

public class GameMenu : MonoBehaviour
{   
    public static event Action OnPlayButtonClick = () => {};
    private void Awake() {
        MenuLvlList.OnReturnToMenuButton += ShowButtons;
    }
    private void OnDestroy() {
        MenuLvlList.OnReturnToMenuButton -= ShowButtons;
    }
    public void HideButtons() {
        OnPlayButtonClick.Invoke();
        gameObject.SetActive(false);
    }

    void ShowButtons() {
        gameObject.SetActive(true);
    }

    public void SinglePlayerButtonClick() {
        SoundsAction.ButtonClick();
        PlayerProfile.gameMode = Gamemode.singlePlayer;
        HideButtons();
    }

    public void BotButtonClick() {
        SoundsAction.ButtonClick();
        PlayerProfile.gameMode = Gamemode.bot;
        HideButtons();
    }
}
