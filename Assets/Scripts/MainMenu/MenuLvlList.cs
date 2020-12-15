using System;
using UnityEngine;

public class MenuLvlList : MonoBehaviour // Showing list of levels
{   
    public static event Action OnReturnToMenuButton = () => {};
     private void Awake() {
        GameMenu.OnPlayButtonClick += ShowList;
        HideList(false);
    }
    private void OnDestroy() {
        GameMenu.OnPlayButtonClick -= ShowList;
    }

    public void HideList(bool sound) {
        if(sound) {
            SoundsAction.ButtonClick();
        }
        gameObject.SetActive(false);
        OnReturnToMenuButton.Invoke();
    }

    public void ShowList() {
        gameObject.SetActive(true);
    }
}
