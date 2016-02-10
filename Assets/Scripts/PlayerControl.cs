using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    GameObject gameOverText;

    public int health;
    //knockback function
    private Vector3 knockback = new Vector3(1, 0,0);
    private Vector3 startPos;
    //how quickly player moves up after knockback
    private float advanceSpeed = 0.2f;


    //how far to move up and down to switch lanes
    private float laneWidth = 1.5f;
    private float shootForce = 5;
    
    //should store lane player is currently in, assign to starting lane value
    //lanes are 1-5 going up
    private float currentLane = 3;

    GameObject playerBullet;
    float speed = 5f;

    ///Swipe checks
    float swipeDist;
    bool stopMovement = false;
    float distanceForSwipe = 2.0f;

    float maxSwipeTime;
    float minSwipeDist;

    // Use this for initialization
    void Start()
    {
        playerBullet = Resources.Load("PlayerBullet") as GameObject;
        //sets player initial position
        startPos = transform.position;
        gameOverText = GameObject.Find("GameOverText");
        gameOverText.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Reset");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        //advance player position if player has been knocked back
        if(transform.position.x < startPos.x)
        {
            transform.Translate(advanceSpeed * Time.deltaTime, 0f, 0f);
        }
        ///Testing movement controls (avoids need for touch)
        /// ///////////////////////////////////////////////////
        /*
        if (Input.GetKeyDown("up"))
        {
            if (currentLane < 5)
            {
                //boolean is true for up, false for down
                SwitchLanes(true);
                currentLane++;
            }

        }
        if (Input.GetKeyDown("down"))
        {
            if (currentLane > 1)
            {
                //boolean is true for up, false for down
                SwitchLanes(false);
                currentLane--;
            }
        }
        //firing

        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
        */
        /////////////////////////////////////////////////////////
       
        Touch[] touch = Input.touches;
        for (int i = 0; i < Input.touchCount; i++)
        {
            swipeDist = touch[i].deltaPosition.y;
            if (touch[i].phase == TouchPhase.Moved)
            {
                if (swipeDist > distanceForSwipe) //swipe left
                {
                    Debug.Log("Swipe Up");
                    if (currentLane < 5 && !stopMovement)
                    {
                        //boolean is true for up, false for down
                        SwitchLanes(true);
                        currentLane++;
                        stopMovement = true;
                    }
                }
                if (swipeDist < -distanceForSwipe) //swipe right
                {
                    Debug.Log("Swipe Down");
                    if (currentLane > 1 && !stopMovement)
                    {
                        //boolean is true for up, false for down
                        SwitchLanes(false);
                        currentLane--;
                        stopMovement = true;
                    }
                }
             }
            if(touch[i].phase == TouchPhase.Ended)
            {
                
                if(swipeDist < distanceForSwipe && swipeDist > -distanceForSwipe)
                {
                    FireBullet();
                }
                stopMovement = false;
            }
        }

    }
    public void FireBullet()
    {
        //...setting shoot direction
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        //...instantiating the rocket
        GameObject bulletInstance = Instantiate(playerBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);
    }
    public void SwitchLanes(bool up)
    {
        //move player up
        if (up == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + laneWidth, transform.position.z);
        }
        else//move player down
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - laneWidth, transform.position.z);
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "EnemyBullet")
        {
            Debug.Log("You got hit");
            transform.position = transform.position - knockback;
            Destroy(col.gameObject);
        }

        if(col.gameObject.name == "DeathZone" || col.gameObject.tag == "Enemy")
        {
            Debug.Log("YOU DIED!!!!!");
            Destroy(this.gameObject);
            gameOverText.SetActive(true);

        }
    }

}
