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
                    if (pixelC.Equals(wallPx))
                    {
                        Debug.Log("I'm a Wall");
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
