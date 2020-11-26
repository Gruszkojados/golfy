using UnityEngine;

public class SounsData : MonoBehaviour
{   
    public GameObject soundsON;
    public GameObject soundsOFF;
    private void Awake() {
        ChangeButtonImage();
    }
    void ChangeSoundsSettings() { // change sounds 
        PlayerProfile.ChangeSounds();
        SoundsAction.ButtonClick();
        ChangeButtonImage();
    }
    void ChangeButtonImage() { // change sound button image
        if(PlayerProfile.sounds) {
            soundsON.SetActive(true);
            soundsOFF.SetActive(false);
        } else {
            soundsON.SetActive(false);
            soundsOFF.SetActive(true);
        }
    }
}
