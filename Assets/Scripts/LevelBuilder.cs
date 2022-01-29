using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    Texture2D levelLayout;
    [SerializeField]
    Color32 wallPx;         //wall pixel color on the layout
    [SerializeField]
    GameObject wallObj;     //wall game object
    [SerializeField]
    Color32 enemyPx;        //enemy pixel color on the layout
    [SerializeField]
    GameObject enemyObj;    //enemy game object
    [SerializeField]
    Color32 potionPx;       //potion pixel color on the layout
    [SerializeField]
    GameObject potionObj;   //potion game object
    [SerializeField]
    Color32 goalPx;         //goal pixel color on the layout
    [SerializeField]
    GameObject goalObj;     //goal game object
    [SerializeField]
    Color32 playerPx;       //player pixel color on the layout
    [SerializeField]
    GameObject playerObj;   //player game object

    // Set up references
    private void Awake()
    {
        // Load resources
        levelLayout = Resources.Load("LevelLayouts/Test") as Texture2D;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Check resources
        if (levelLayout == null)
        {
            Debug.Log("WARNING: Level layout can't be found!");
        }
        else
        {
            Debug.Log("Level layout: " + levelLayout + " (" + levelLayout.width + "x" + levelLayout.height + " px)");
        }

        // Build the level
        if (levelLayout != null)
        {
            BuildLevel();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void BuildLevel()
    {
        // Iterate through the layout
        for (int i = 0; i < levelLayout.width; i++)
        {
            for (int j = 0; j < levelLayout.height; j++)
            {
                // Get color from pixel
                Color32 pixelC = levelLayout.GetPixel(i, j);

                if (pixelC.a != 0)  //if pixel is NOT empty
                {
                    // Wall object
                    if (pixelC.Equals(wallPx) && wallObj != null)
                    {
                        Instantiate(wallObj, new Vector2(i, j), Quaternion.identity);
                        Debug.Log(wallObj.name + " has been instanciated @(" + i + "," + j + ")");
                    }

                    // Enemy object
                    else if (pixelC.Equals(enemyPx) && enemyObj != null)
                    {
                        Instantiate(enemyObj, new Vector2(i, j), Quaternion.identity);
                        Debug.Log(enemyObj.name + " has been instanciated @(" + i + "," + j + ")");
                    }

                    // Potion object
                    else if (pixelC.Equals(potionPx) && potionObj != null)
                    {
                        Instantiate(potionObj, new Vector2(i, j), Quaternion.identity);
                        Debug.Log(potionObj.name + " has been instanciated @(" + i + "," + j + ")");
                    }

                    // Goal object
                    else if (pixelC.Equals(goalPx) && goalObj != null)
                    {
                        Instantiate(goalObj, new Vector2(i, j), Quaternion.identity);
                        Debug.Log(goalObj.name + " has been instanciated @(" + i + "," + j + ")");
                    }

                    // Player object
                    else if (pixelC.Equals(playerPx) && playerObj != null)
                    {
                        Instantiate(playerObj, new Vector2(i, j), Quaternion.identity);
                        Debug.Log(playerObj.name + " has been instanciated @(" + i + "," + j + ")");
                    }

                    // Catch layout looseness
                    else
                    {
                        Debug.Log("Px (" + i + "," + j + ") can't be interpreted!");
                    }
                }
            }
        }
    }
}
