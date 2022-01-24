using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;        //base displacement speed
    [SerializeField]
    private float baseAngular;  //base angular speed (ration)
    private int clockwise = -1; //direction of the rotation (-1 is clockwise)
    [SerializeField]
    private Transform anchor;   //arm's holder
    private Vector2 direction;  //direction of displacement

    // Set up references
    void Awake()
    {

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
}
