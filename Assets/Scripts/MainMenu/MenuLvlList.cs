using System;
using UnityEngine;

public class MenuLvlList : MonoBehaviour
{   
    public static event Action OnReturnToMenuButton = () => {};
     private void Awake() {
        GameMenu.OnPlayButtonClick += ShowList;
        HideList();
    }
    private void OnDestroy() {
        GameMenu.OnPlayButtonClick -= ShowList;
    }

    public void HideList() {
        SoundsAction.ButtonClick();
        gameObject.SetActive(false);
        OnReturnToMenuButton.Invoke();
    }

    public void ShowList() {
        gameObject.SetActive(true);
    }
}
