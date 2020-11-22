using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    void Update()
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
