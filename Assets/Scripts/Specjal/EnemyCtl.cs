using UnityEngine;

public class EnemyCtl : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 1f;

    private void Start() {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }
    void SpawnEnemy() {
        Instantiate(enemy);
    }
}
