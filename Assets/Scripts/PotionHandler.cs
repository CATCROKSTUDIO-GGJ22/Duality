using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHandler : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private int wing;
    [SerializeField]
    private CanvasRenderer[] stages = new CanvasRenderer[6];    // lenght must be one more than mana points

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
        // Reset gauge
        foreach (CanvasRenderer stage in stages)
        {
            stage.SetAlpha(0f);
        }

        // Check player's stats
        if (player != null)
        {
            int mana = player.GetComponent<PlayerStats>().GetWingMana(wing);
            stages[mana].SetAlpha(1f);
        }

        // Restore container
        stages[0].SetAlpha(1f);
    }
}
