using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int baseDamage;             //damage applied to the player if they're hit
    [SerializeField]
    private float verticalSpeed;        //vertical speed
    private int verticalDirection;
    [SerializeField]
    private float horizontalSpeed;      //horizontal speed
    private int horizontalDirection;
    [SerializeField]
    private bool sinusoidal;            //if the movement is sinusoidal
    [SerializeField]
    private bool turnsBackOnWalls;      //if turns back when colliding with a Wall or other Enemies
    [SerializeField]
    private bool turnsBackOnPlayer;     //if turns back when colliding with the Player
    [SerializeField]
    private bool killSelfOnHitWalls;    //if it's destroyed after hitting a Wall (or reaching the level limits)
    [SerializeField]
    private bool killSelfOnHitPlayer;   //if it's destroyed after hitting the player
    SpriteRenderer sprite;
    private bool hit;                   //makes sure it will only turn back once per frame (despite the number of colliders it enters)
    private bool playerHit;             //if has hit a player on previous frame

    // Set up references
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Check values
        if (turnsBackOnPlayer && killSelfOnHitPlayer)
        {
            turnsBackOnPlayer = false;
            Debug.Log("WARNING: TurnsBackOnPlayer and KillOnHitPlayer are both checked! It won't be visible when turning back after hitting a Player so it has been deactivated");
        }
        if (turnsBackOnWalls && killSelfOnHitWalls)
        {
            turnsBackOnWalls = false;
            Debug.Log("WARNING: TurnsBackOnWalls and KillOnHitWalls are both checked! It won't be visible when turning back after hitting a Wall so it has been deactivated");
        }

        // Randomize initial directions
        verticalDirection = Random.Range(0, 2) * 2 - 1;
        horizontalDirection = Random.Range(0, 2) * 2 - 1;

        // Initialize control values
        hit = false;
        playerHit = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerHit)  //checks if has been hit by a Player on previous frame
        {
            if (killSelfOnHitPlayer)
            {
                Destroy(this.gameObject);
            }
            else
            {
                if (turnsBackOnPlayer)
                {
                    TurnBack();
                }
                //trigger any effect after hitting a Player -HERE- (WIP)
            }
            playerHit = false;
        }

        hit = false;    //resets control boolean

        if (sinusoidal)
        {
            //WIP
        }
        else
        {
            // Vertical displacement
            if (verticalSpeed > 0f)
            {
                this.transform.Translate(this.transform.up * verticalDirection * verticalSpeed * Time.deltaTime);
            }

            // Horizontal displacement
            if (horizontalSpeed > 0f)
            {
                this.transform.Translate(this.transform.right * horizontalDirection * horizontalSpeed * Time.deltaTime);
            }
        }

        // Enemies will always turn back when reaching a World limit! (WIP)
        //TurnBack();
    }

    // LateUpdate is called right before the render engine
    private void LateUpdate()
    {
        // Flip sprite towars horizontal movement direction
        if (horizontalDirection > 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Walls
        if (col.gameObject.tag == "Hazard")
        {
            if (killSelfOnHitWalls)
            {
                Destroy(this.gameObject);
            }
            else if (turnsBackOnWalls)
            {
                TurnBack();
            }
        }

        // (the level border objects have been deprecated - use new World Size @ Game Manager to calculate level limits!)
    }

    // Helper function for turning back ater a hit
    private void TurnBack()
    {
        if (!hit)
        {
            if (verticalSpeed > 0f)
            {
                verticalDirection = -verticalDirection;
            }

            if (horizontalSpeed > 0f)
            {
                horizontalDirection = -horizontalDirection;
            }

            hit = true;
        }
    }

    public int GetDamage()
    {
        playerHit = true;
        return baseDamage;
    }
}
