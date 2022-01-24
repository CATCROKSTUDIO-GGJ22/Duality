using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingController : MonoBehaviour
{
    PlayerStats playerStats;
    private Vector2 basePos;

    // Set up references
    void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        basePos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = basePos; //fixes displacement when hitting a physical object (WIP - could easily be improved)
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hazard")
        {
            Debug.Log("PING");
        }
    }
}
