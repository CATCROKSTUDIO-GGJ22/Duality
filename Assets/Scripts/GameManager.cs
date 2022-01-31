using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;  // Static instance of GameManager which allows it to be accessed by any other script
    private Text text;
    private bool next;
    private bool gameover;

    private void Awake()
    {
        // Check if instance already exists and instances it if it doesn't
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(this.gameObject);

        // Set up references
        text = GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        next = false;
        gameover = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (next || gameover)   //WIP
        {
            Time.timeScale = 0;
            if (Input.anyKeyDown)
            {
                Application.Quit();
            }
        }
    }

    // LateUpdate is called right before the render engine
    private void LateUpdate()
    {
        if (next || gameover)   //WIP
        {
            if (next)
            {
                text.text = "CONGRATULATIONS\nYou beat our prototype\nThanks for playing!\n(presh any button to exit this program)";
            }
            else
            {
                text.text = "GAME OVER\nThanks for playing our prototype!\n(presh any button to exit this program)";
            }
            text.enabled = true;
        }
    }

    public void LevelEndingRoutine(bool success)
    {
        if (success)
        {
            next = true;
        }
        else
        {
            gameover = true;
        }
    }
}
