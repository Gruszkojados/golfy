using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Finder : MonoBehaviour
{   
    public GameObject pathFider;
    
    private void Awake() {
        WallCollider.OnColiderDrowed += ShowGameObject;
    }

    private void OnDestroy() {
        WallCollider.OnColiderDrowed -= ShowGameObject;
    }

    public void ShowGameObject() {
        pathFider.SetActive(true);
    }

}
