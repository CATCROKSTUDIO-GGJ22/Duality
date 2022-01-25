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

    // Updates the PlayerStats and sends the changes to the PlayerController
    public void Hit(int wingID, Vector2 hittingPoint)
    {
        switch (wingID)
        {
            case 0:
                currentLeftMana -= baseDamageTaken;
                //WIP
                break;
            case 1:
                currentRightMana -= baseDamageTaken;
                //WIP
                break;
        }
        playerController.RefreshRotation(hittingPoint);
    }

    // Returns the mana value of the Wing with the given ID (or -1 if error happens)
    public int GetWingMana(int wingID)
    {
        switch (wingID)
        {
            case 0:
                return currentLeftMana;
            case 1:
                return currentRightMana;
        }
        return -1;
    }
}
