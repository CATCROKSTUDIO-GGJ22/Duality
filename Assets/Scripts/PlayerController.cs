using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerStats playerStats;
    SpriteRenderer face;
    [SerializeField]
    private float speed;            //base displacement speed
    [SerializeField]
    private float baseAngular;      //base angular velocity (ration)
    private float angular1;         //internal angular velocity for Left wing's cycle
    private float angular2;         //internal angular velocity for Right wing's cycle
    [SerializeField]
    private float rotationCycle;    //duration of the rotation cycle (on degrees)
    private float currentCycle;     //counter variable for the rotation cycle
    [SerializeField]
    private bool resetCycle;        //defines if the rotation cycle is reset to always the slowest or continues after getting hit
    private int clockwise = -1;     //direction of the rotation (-1 is clockwise)
    private float asynchrony = 0f;  //difference factor between Wings
    [SerializeField]
    private Transform anchor;       //arm's holder
    private Vector2 direction;      //direction of displacement
    [SerializeField]
    private float repulsionForce;   //force of repulsion when getting hit

    // Set up references
    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        face = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        angular1 = baseAngular;
        angular2 = baseAngular;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation
        float newRotation = angular1 * clockwise * Time.deltaTime;
        currentCycle += Mathf.Abs(newRotation);
        int cycles = 0;
        while (currentCycle >= rotationCycle)
        {
            currentCycle -= rotationCycle;
            cycles++;
        }
        if (cycles > 0)
        {
            if (cycles > 1)
            {
                Debug.Log("WARNING: " + cycles + " cycles happened during last frame!");
            }
            float swap = angular1;
            angular1 = angular2;
            angular2 = swap;
            Debug.Log("Cycle has been swapped to " + angular1);
        }
        anchor.Rotate(Vector3.forward * newRotation, Space.Self);

        // Displacement
        if (Input.GetAxisRaw("Horizontal") < 0f && !face.flipX)
        {
            face.flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0f && face.flipX)
        {
            face.flipX = false;
        }
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        this.transform.Translate(direction * speed * Time.deltaTime);
    }

    // Updates the rotation cycles
    public void ResetRotation(Vector2 hittingPoint)
    {
        // Apply repulsion force
        Vector2 repulsion = new Vector2(this.transform.position.x, this.transform.position.y) - hittingPoint;
        GetComponent<Rigidbody2D>().AddForce(repulsion.normalized * repulsionForce);

        // Recalculate Asynchrony
        asynchrony = playerStats.CalculateNewAsynchrony();
        Debug.Log("Asynchrony: " + asynchrony);

        // Set new Angulars
        if (resetCycle || asynchrony == 0f || angular1 < angular2)
        {
            angular1 = baseAngular - baseAngular * asynchrony;
            angular2 = baseAngular + baseAngular * asynchrony;
        }
        else
        {
            angular1 = baseAngular + baseAngular * asynchrony;
            angular2 = baseAngular - baseAngular * asynchrony;
        }
        currentCycle = 0f;

        // Swap direction of rotation
        clockwise = -clockwise;
    }
}
