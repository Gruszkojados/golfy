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

    void EnableDisableTouchInput(Player player) {
        if(player is HumanPlayer) {
            touchInput.enabled = true;
        } else {
            touchInput.enabled = false;    
        }
    }
}
