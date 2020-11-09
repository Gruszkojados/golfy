using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public void ShowHideMenu() {
        SoundsAction.ButtonClick();
        if(gameObject.activeSelf) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }
}
