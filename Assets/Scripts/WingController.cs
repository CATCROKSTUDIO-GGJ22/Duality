using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingController : MonoBehaviour
{
    PlayerStats playerStats;
    private Vector2 basePos;
    [SerializeField]
    private int wingID;         //this wing's identifier

    // Set up references
    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        basePos = this.transform.localPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        this.transform.localPosition = basePos; //fixes displacement when hitting a physical object (WIP - could easily be improved)
    }

    // Sends the Hit event to the PlayerStats with the wing id
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hazard")
        {
            playerStats.Hit(wingID, col.transform.position);    //can be improved by sending the exact point of collision (WIP)
        }
    }
}
