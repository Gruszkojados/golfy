using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public GameObject[] images;
    Image image;
    int currentImageIndex = 0;
    void Start() {
        images[currentImageIndex].SetActive(true);
    }
    public void ChangeImage(bool isUp) { // change tutorial image
        SoundsAction.ButtonClick();
        if(isUp) {
            if(currentImageIndex == images.Length-1) {
                return;
            }
            currentImageIndex += 1;
            images[currentImageIndex-1].SetActive(false);
            images[currentImageIndex].SetActive(true);
        } else {
            if(currentImageIndex == 0) {
                return;
            }
            currentImageIndex -= 1;
            images[currentImageIndex+1].SetActive(false);
            images[currentImageIndex].SetActive(true);
        }
    }
}
