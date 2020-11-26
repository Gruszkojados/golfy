using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{   
    public static event System.Action OnDie = () => {};
    [SerializeField] int hitPoits;
    private void Awake() {
        Vector2 vec = new Vector2(Random.Range(-1.5f,1.5f), 4f);
        transform.position = vec;
    }
    private void Start() {
        hitPoits = (int)Mathf.Abs(Random.Range(1f, 9f)) * 10;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        hitPoits -= 10;
        if(hitPoits <= 0) {
            OnDie.Invoke();
            SoundsAction.SetForce();
            Destroy(gameObject);
        }
        if(other.tag == "Player") {
            SceneManager.LoadScene(2);
        }
    }
}
