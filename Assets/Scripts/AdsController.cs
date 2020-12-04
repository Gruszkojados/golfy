using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour
{
    string gameId = "3925763";
    bool testMode = true;

    void Start () {
        LvlController.OnAdShow += AdShow;
        Advertisement.Initialize(gameId, testMode);
    }

    void OnDestroy() {
        LvlController.OnAdShow -= AdShow;
    }

    void AdShow(int type) {
        if(Advertisement.IsReady()) {
            if(type.Equals(1)) {
                Debug.Log("Show ad");
                Advertisement.Show();
            } else {
                Debug.Log("Film ad");
            }
        }
    }
}
