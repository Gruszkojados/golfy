using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Application.platform == RuntimePlatform.Android) {
                if(SceneManager.GetActiveScene().buildIndex == 1) {
                    SceneManager.LoadScene(0);
                } else {
                    Application.Quit();
                }
            } 
        }
    }
}
