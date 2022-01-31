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
        // Enemies
        if (playerStats.IsReady() && col.gameObject.tag == "Hazard")    //check if IsReady to prevent collision accumulation on a single frame
        {
            playerStats.Hit(wingID, col.transform.position);            //could be improved by sending the exact point of collision (WIP)
        }

        // Potions
        else if (col.gameObject.tag == "Potion")                        //the potion heals the Wing that hits them and then the potion itself is destroyed
        {
            playerStats.Heal(wingID);
            Destroy(col.gameObject);
        }

        // Teleports
        else if (col.gameObject.tag == "Goal")
        {
            playerStats.Teleport();
        }
    }
}
