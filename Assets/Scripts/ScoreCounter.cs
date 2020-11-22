using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private void Awake() {
        if(PlayerProfile.gameMode == Gamemode.bot) {
            gameObject.SetActive(false);
        }
    }
}
