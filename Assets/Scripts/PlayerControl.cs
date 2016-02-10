using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    public int health;
    //knockback function
    private Vector3 knockback = new Vector3(1, 0,0);
    private Vector3 startPos;
    //how quickly player moves up after knockback
    private float advanceSpeed = 0.2f;


    //how far to move up and down to switch lanes
    private float laneWidth = 1.5f;
    private float shootForce = 5;
    //distance a touch must move to be considered a swipe
    private float distanceForSwipe = 10;
    //should store lane player is currently in, assign to starting lane value
    //lanes are 1-5 going up
    private float currentLane = 3;

    GameObject playerBullet;
    float speed = 10f;

    ///Swipe checks
    bool couldBeSwipe = false;
    float swipeStartTime;
    float maxSwipeTime;
    float minSwipeDist;

    // Use this for initialization
    void Start()
    {
        playerBullet = Resources.Load("PlayerBullet") as GameObject;
        //sets player initial position
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //advance player position if player has been knocked back
        if(transform.position.x < startPos.x)
        {
            transform.Translate(advanceSpeed * Time.deltaTime, 0f, 0f);
        }
        ///Testing movement controls (avoids need for touch)
        /// ///////////////////////////////////////////////////
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
        /////////////////////////////////////////////////////////

        /*
        //check if there are touches

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved )
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        //if player swipes up

            if (touchDeltaPosition.y > distanceForSwipe)
            {
                if (currentLane < 5)
                {
                    //boolean is true for up, false for down
                    SwitchLanes(true);
                    currentLane++;
                }
            }
            //if player swipes down
            if (touchDeltaPosition.y < distanceForSwipe)
            {
                if (currentLane > 1)
                {
                    //boolean is true for up, false for down
                    SwitchLanes(false);
                    currentLane--;
                }
            }



        }
        */

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

        if(col.gameObject.name == "DeathZone" || col.gameObject.name == "BasicEnemy")
        {
            Debug.Log("YOU DIED!!!!!");
            Destroy(this.gameObject);
          
        }
    }

}
