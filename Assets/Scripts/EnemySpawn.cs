using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    float minRespawn = 5;
    float maxRespawn = 20;

    GameObject basicEnemy;

    void Start()
    {
        basicEnemy = Resources.Load("BasicEnemy") as GameObject;
        Invoke("SpawnEnemy", (Random.Range(minRespawn, maxRespawn)));
    }

    void SpawnEnemy()
    {
        float delay = Random.Range(minRespawn, maxRespawn);
        //Do stuff
        Instantiate(basicEnemy, transform.position, Quaternion.identity);
        //
        Invoke("SpawnEnemy", delay);
    }
}
