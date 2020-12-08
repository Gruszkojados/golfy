using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private void Awake() { // Score counter is enable only in "Single" mode.
        if(PlayerProfile.gameMode == Gamemode.bot) {
            gameObject.SetActive(false);
        }
    }
}
