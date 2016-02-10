using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float speed = 1.0f;
    GameObject enemyBullet;
    GameObject player;

    float minFireRate = 5;
    float maxFireRate = 10;

    // Use this for initialization
    void Start ()
    {
        enemyBullet = Resources.Load("EnemyBullet") as GameObject;
        player = GameObject.Find("Player");
        InvokeRepeating("FireBullet", (Random.Range(minFireRate, maxFireRate)), (Random.Range(minFireRate, maxFireRate)));
    }
	
	// Update is called once per frame
	void Update ()
    {

        //move left constantly
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);

    }
    void FireBullet()
    {
        if (player != null)
        {
            //...setting shoot direction
            Vector3 shootDirection;
            shootDirection = player.transform.position;
            shootDirection.z = 0.0f;
            shootDirection = shootDirection - transform.position;
            //...instantiating the rocket
            GameObject bulletInstance = Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("Killed Enemy");
            //destroy bullet
            Destroy(col.gameObject);
            //destory enemy
            Destroy(this.gameObject);
        }
        if (col.gameObject.name == "DeathZone")
        {
           
            Destroy(this.gameObject);

        }
    }
}
