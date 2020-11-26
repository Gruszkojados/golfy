using UnityEngine;
using golf;

public class PlayerInput : MonoBehaviour
{
    public TouchInput touchInput;

    private void Awake() {
        PlayerController.OnChangePlayer += EnableDisableTouchInput;
    }

    private void OnDestroy() {
        PlayerController.OnChangePlayer -= EnableDisableTouchInput;
    }

    void EnableDisableTouchInput(Player player) { // enable/disable touch between turns
        if(player is HumanPlayer) {
            touchInput.enabled = true;
        } else {
            touchInput.enabled = false;
        }
    }
}
