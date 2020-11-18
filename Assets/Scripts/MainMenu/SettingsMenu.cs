using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject tutorial;
    public void ShowHideMenu() {
        SoundsAction.ButtonClick();

        if(tutorial.activeSelf) {
            tutorial.SetActive(false);
            return;
        }

        if(gameObject.activeSelf) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }
}
