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
    void ChangeTitle(string title) {
        textMeshProUGUI.text = title;
    }
}
