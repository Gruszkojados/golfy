using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    void Update() // functon is checking for using exit button on device
    {   
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Application.platform == RuntimePlatform.Android) {
                int index = SceneManager.GetActiveScene().buildIndex;
                if(index == 1 || index == 2) {
                    SceneManager.LoadScene(0);
                } else {
                    Application.Quit();
                }
            } 
        }
    }
}
