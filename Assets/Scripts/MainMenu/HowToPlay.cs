using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public GameObject tutorialObject;
    public void ActiveTutorial() {
        tutorialObject.SetActive(true);
    }
}
