using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerStats playerStats;
    [SerializeField]
    private float speed;            //base displacement speed
    [SerializeField]
    private float baseAngular;      //base angular speed (ration)
    private int clockwise = -1;     //direction of the rotation (-1 is clockwise)
    [SerializeField]
    private Transform anchor;       //arm's holder
    private Vector2 direction;      //direction of displacement
    [SerializeField]
    private float repulsionForce;   //force of repulsion when getting hit

    // Set up references
    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Rotation
        anchor.Rotate(Vector3.forward * baseAngular * clockwise * Time.deltaTime, Space.Self); //WIP

        // Displacement
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        this.transform.Translate(direction * speed * Time.deltaTime);
    }

    // Updates the rotation cycles
    public void RefreshRotation(Vector2 hittingPoint)
    {
        // Apply repulsion force
        Vector2 repulsion = new Vector2(this.transform.position.x, this.transform.position.y) - hittingPoint;
        GetComponent<Rigidbody2D>().AddForce(repulsion.normalized * repulsionForce);

        // Swap direction of rotation
        clockwise = -clockwise;
        //WIP
    }
}
