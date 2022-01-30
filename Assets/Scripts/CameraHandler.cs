using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private GameObject player;

    // Set up references
    private void Awake()
    {

    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // LateUpdate is called right before the render engine
    private void LateUpdate()
    {
        if (player != null)
        {
            MoveTowards(player);
        }
    }

    // Helper function to move towards an object
    private void MoveTowards(GameObject target)
    {
        this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z); //WIP
    }
}
