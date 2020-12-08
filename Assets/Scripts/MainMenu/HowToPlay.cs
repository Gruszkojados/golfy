using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public GameObject tutorialObject;
    public void ActiveTutorial() {
        SoundsAction.ButtonClick();
        tutorialObject.SetActive(true);
    }
}
