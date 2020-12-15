using UnityEngine;

public class HowToPlay : MonoBehaviour // Tutorial button
{
    public GameObject tutorialObject;
    public void ActiveTutorial() {
        SoundsAction.ButtonClick();
        tutorialObject.SetActive(true);
    }
}
