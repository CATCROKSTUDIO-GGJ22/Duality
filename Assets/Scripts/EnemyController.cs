using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float verticalSpeed;    //vertical speed
    private int verticalDirection;
    [SerializeField]
    private float horizontalSpeed;  //horizontal speed
    private int horizontalDirection;
    [SerializeField]
    private bool sinusoidal;        //if the movement is sinusoidal
    [SerializeField]
    private bool turnsBackOnWalls;  //if turns back when colliding with a Wall or other Enemies
    [SerializeField]
    private bool turnsBackOnPlayer; //if turns back when colliding with the Player
    [SerializeField]
    private bool destroyedOnHit;    //if it's destroyed after hitting the player

    // Set up references
    private void Awake()
    {

    }

    // Start is called before the first frame update
    private void Start()
    {
        // Check values
        if (turnsBackOnPlayer && destroyedOnHit)
        {
            turnsBackOnPlayer = false;
            Debug.Log("WARNING: TurnsBackOnPlayer and DestroyedOnHit are both checked! It won't be visible when turning back after hitting a Player so it has been deactivated");
        }

        // Randomize initial directions
        verticalDirection = Random.Range(0, 2) * 2 - 1;
        horizontalDirection = Random.Range(0, 2) * 2 - 1;
    }

    // Update is called once per frame
    private void Update()
    {
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
            if (verticalSpeed > 0f)
            {
                this.transform.Translate(this.transform.right * horizontalDirection * horizontalSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Player
        if (turnsBackOnPlayer && col.gameObject.tag == "Player")
        {
            Debug.Log("TURN BACK");//WIP
        }

        // Walls/Enemies
        else if (turnsBackOnWalls && col.gameObject.tag == "Hazard")
        {
            Debug.Log("TURN BACK");//WIP
        }

        // Borders
        else if (col.gameObject.tag == "Border")    //it should always turn back on borders!
        {
            Debug.Log("TURN BACK");//WIP
        }
    }
}
