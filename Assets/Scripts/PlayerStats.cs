using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField]
    private int baseLeftMana;       //base mana for the Left wing
    [SerializeField]
    private int currentLeftMana;    //current Left mana
    [SerializeField]
    private int baseRightMana;      //base mana for the Right wing
    [SerializeField]
    private int currentRightMana;   //current Right mana
    [SerializeField]
    private int baseDamageTaken;    //base damage being substracted when getting hit

    // Set up references
    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentLeftMana = baseLeftMana;
        currentRightMana = baseRightMana;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Calculates the asynchrony factor between the two Wings
    // the more mana difference, the more angular asynchrony between Wings
    // asynchrony is always balanced (which means a 70% asynchrony translates into +35% -35% with independence between the specific stats - only cares about difference)
    // the theoretical maximun is 100% asynchrony (+50% -50%) corresponding the higher to the faster Angular, where 100% is the base Angular
    // the id indicates the Wing being hit, which starts the new rotation cycle
    private float CalculateNewAsynchrony(int id)
    {
        return 0;
    }

    // Updates the PlayerStats and sends the changes to the PlayerController
    // the id indicates the Wing being hit, which mana is being substracted
    public void Hit(int id, Vector2 hittingPoint)
    {
        switch (id)
        {
            case 0:
                currentLeftMana -= baseDamageTaken;
                //check for game over condition (WIP)
                break;
            case 1:
                currentRightMana -= baseDamageTaken;
                //check for game over condition (WIP)
                break;
        }
        playerController.ResetRotation(hittingPoint);

        // Trigger invincibility time
        //(WIP)
    }

    // Returns the mana value of the Wing with the given ID (or -1 if error happens)
    // the id indicates the Wing being asked for
    /*public int GetWingMana(int id)
    {
        switch (id)
        {
            case 0:
                return currentLeftMana;
            case 1:
                return currentRightMana;
        }
        return -1;
    }*/
}
