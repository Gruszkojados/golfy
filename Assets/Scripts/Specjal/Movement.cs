using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{    
    public float speed = 3f;   
    public float shootRate = 0.08f;
    public GameObject shootPrefab;
    Vector3 position;
    bool isShooting = false;
    bool canInstantiate = true;
    private void Awake() {
        position = transform.localPosition;
    }
    void FixedUpdate() {
        BeterMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    private void Update() {
        #if UNITY_EDITOR
            if(Input.GetKeyDown("space")) {
            isShooting = true;
            }
            if(Input.GetKeyUp("space")) {
                isShooting = false;
            }
            if(isShooting) {
                Shoot();
            }
        #else
            if(Input.touchCount == 1) {
                AndroidMove(Input.GetTouch(0).position);
            }
            Shoot();
        #endif
    }
    void AndroidMove(Vector2 vec) {
        if(vec.x < 500) {
            BeterMove(-0.5f, 0);
        } else {
            BeterMove(0.5f, 0);
        }
    }
    void BeterMove(float x, float y) {
        float targetX = transform.localPosition.x + x * Time.deltaTime * speed;
        position.x = Mathf.Lerp(transform.localPosition.x, targetX, Time.deltaTime * 1000);
        float targetY = transform.localPosition.y + y * Time.deltaTime * speed;
        position.y = Mathf.Lerp(transform.localPosition.y, targetY, Time.deltaTime * 1000);
        transform.localPosition = position;
    }
    void Shoot() {
        if(canInstantiate) {
            StartCoroutine(shootWait());
        }
    }
    public IEnumerator shootWait() {
        canInstantiate = false;
        Vector2 vec = transform.position;
        Instantiate(shootPrefab, vec, new Quaternion());
        yield return new WaitForSeconds(shootRate);
        canInstantiate = true;
    }
}
