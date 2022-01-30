using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    private GameObject spawnObj;    //object to spawn
    private GameObject wall0;
    private GameObject wallSE;
    private GameObject wallSW;
    private GameObject wallNW;
    private GameObject wallNE;
    private GameObject wallNX;
    private GameObject wallEX;
    private GameObject wallSX;
    private GameObject wallWX;
    [SerializeField]
    private bool[] neighbors = new bool[4]; // N-E-S-W
    [SerializeField]
    private bool adaptative;        //if this block is adaptative

    // Set up references
    private void Awake()
    {
        // Load resources
        wall0 = Resources.Load("Prefabs/Wall0") as GameObject;
        wallSE = Resources.Load("Prefabs/WallSE") as GameObject;
        wallSW = Resources.Load("Prefabs/WallSW") as GameObject;
        wallNW = Resources.Load("Prefabs/WallNW") as GameObject;
        wallNE = Resources.Load("Prefabs/WallNE") as GameObject;
        wallNX = Resources.Load("Prefabs/WallNX") as GameObject;
        wallEX = Resources.Load("Prefabs/WallEX") as GameObject;
        wallSX = Resources.Load("Prefabs/WallSX") as GameObject;
        wallWX = Resources.Load("Prefabs/WallWX") as GameObject;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Make sure self collision is active
        Physics2D.queriesStartInColliders = true;

        // Determine object
        if (adaptative)
        {
            SearchNeighbors();
            SelectObject();
        }
        else
        {
            spawnObj = wall0;
        }

        // Instanciate object
        Instantiate(spawnObj, this.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    // Selects an object based on the boolean array
    private void SelectObject()
    {
        // N-E-S-W

        // SE
        if (!neighbors[0] && neighbors[1] && neighbors[2] && !neighbors[3])
        {
            spawnObj = wallSE;
        }

        // SW
        else if (!neighbors[0] && !neighbors[1] && neighbors[2] && neighbors[3])
        {
            spawnObj = wallSW;
        }

        // NW
        else if (neighbors[0] && !neighbors[1] && !neighbors[2] && neighbors[3])
        {
            spawnObj = wallNW;
        }

        // NE
        else if (neighbors[0] && neighbors[1] && !neighbors[2] && !neighbors[3])
        {
            spawnObj = wallNE;
        }

        // xESW
        else if (!neighbors[0] && neighbors[1] && neighbors[2] && neighbors[3])
        {
            spawnObj = wallNX;
        }

        // NxSW
        else if (neighbors[0] && !neighbors[1] && neighbors[2] && neighbors[3])
        {
            spawnObj = wallEX;
        }

        // NExW
        else if (neighbors[0] && neighbors[1] && !neighbors[2] && neighbors[3])
        {
            spawnObj = wallSX;
        }

        // NESx
        else if (neighbors[0] && neighbors[1] && neighbors[2] && !neighbors[3])
        {
            spawnObj = wallWX;
        }

        else
        {
            spawnObj = wall0;
        }
    }

    // Looks for neighbor blocks and updates the boolean array
    private void SearchNeighbors()
    {
        // N
        RaycastHit2D hitN = GetFirstRaycastHit(this.transform.up);
        if (hitN.distance < 1f)
        {
            neighbors[0] = true;
            Debug.DrawRay(this.transform.position, this.transform.up, Color.red, 1f);
        }
        else
        {
            neighbors[0] = false;
            Debug.DrawRay(this.transform.position, this.transform.up, Color.yellow, 1f);
        }

        // E
        RaycastHit2D hitE = GetFirstRaycastHit(this.transform.right);
        if (hitE.distance < 1f)
        {
            neighbors[1] = true;
            Debug.DrawRay(this.transform.position, this.transform.right, Color.red, 1f);
        }
        else
        {
            neighbors[1] = false;
            Debug.DrawRay(this.transform.position, this.transform.right, Color.yellow, 1f);
        }

        // S
        RaycastHit2D hitS = GetFirstRaycastHit(this.transform.up * -1);
        if (hitS.distance < 1f)
        {
            neighbors[2] = true;
            Debug.DrawRay(this.transform.position, this.transform.up * -1, Color.red, 1f);
        }
        else
        {
            neighbors[2] = false;
            Debug.DrawRay(this.transform.position, this.transform.up * -1, Color.yellow, 1f);
        }

        // W
        RaycastHit2D hitW = GetFirstRaycastHit(this.transform.right * -1);
        if (hitW.distance < 1f)
        {
            neighbors[3] = true;
            Debug.DrawRay(this.transform.position, this.transform.right * -1, Color.red, 1f);
        }
        else
        {
            neighbors[3] = false;
            Debug.DrawRay(this.transform.position, this.transform.right * -1, Color.yellow, 1f);
        }
    }

    // This helper function fixes a bug where self concave colliders are always detected as first hit
    private RaycastHit2D GetFirstRaycastHit(Vector2 direction)
    {
        //Check "Queries Start in Colliders" in Edit > Project Settings > Physics2D
        RaycastHit2D[] hits = new RaycastHit2D[2];
        Physics2D.RaycastNonAlloc(this.transform.position, direction, hits);
        //hits[0] will always be the Collider2D you are casting from.
        return hits[1];
    }
}
