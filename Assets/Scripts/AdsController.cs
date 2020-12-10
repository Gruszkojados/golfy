using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour // Class for monetization.
{
    int adIndex = 0;
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
        Debug.Log("Ad Show Now");
        try {
           adIndex++;
            if(adIndex%4==0) {
                if(Advertisement.IsReady()) {
                    if(type.Equals(1)) {
                        Advertisement.Show();
                    } else {
                        Debug.Log("Film ad");
                    }
                }
            } 
        }
        catch {
            Debug.Log("Ad error");
        }
        
    }
}
