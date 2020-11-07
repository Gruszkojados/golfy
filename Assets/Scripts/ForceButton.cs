using System;
using UnityEngine;

public class ForceButton : MonoBehaviour
{
    public static event Action OnChangeForce = () => { };

    public GameObject[] objectsToEnable;

    public void Setfoce() {
        SoundsAction.SetForce();
        foreach (var item in objectsToEnable)
        {
            item.SetActive(true);
        }
        OnChangeForce.Invoke();
        gameObject.SetActive(false);
    }

    private void Awake() {
        ShootButton.OnShoot += HideButton;
        HumanPlayer.OnHumanStartTurn += ShowForceButton;
        LvlController.OnHideQuickMenu += ShowForceButton;
        LvlController.OnResetLvl += ShowForceButton;
    }

    private void OnDestroy() {
        ShootButton.OnShoot -= HideButton;
        HumanPlayer.OnHumanStartTurn -= ShowForceButton;
        LvlController.OnHideQuickMenu -= ShowForceButton;
        LvlController.OnResetLvl -= ShowForceButton;
    }

    void HideButton(float _) {
        gameObject.SetActive(false);
    }

    void ShowForceButton() {
        gameObject.SetActive(true);
    }
}
