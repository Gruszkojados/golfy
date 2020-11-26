using UnityEngine;

public class LoadingTrigger : MonoBehaviour
{
    public Animator transition;
    private void Awake() {
        LvlButton.OnLoadLvlButton += MakeTrasition;
        LvlController.OnHomeBack += MakeTrasition;
    }
    private void OnDestroy() {
        LvlButton.OnLoadLvlButton -= MakeTrasition;
        LvlController.OnHomeBack -= MakeTrasition;
    }
    public void MakeTrasition() { // function running loading animation
        transition.SetTrigger("Start");
    }
}
