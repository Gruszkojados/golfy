using TMPro;
using UnityEngine;

public class ButtonTitle : MonoBehaviour
{   
    TextMeshProUGUI textMeshProUGUI;
    void Start() {
        SettingsMenu.OnHitButton += ChangeTitle;
        textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
    }
    void OnDestroy() {
        SettingsMenu.OnHitButton -= ChangeTitle;
    }
    void ChangeTitle(string title) { // seting settings button title in GUI
        textMeshProUGUI.text = title;
    }
}
