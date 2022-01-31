using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    GameManager gameManager;
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
    [SerializeField]
    private int baseRecovery;       //base damage being recovered by potions
    [SerializeField]
    private bool continuousBouncing;//defines if can be bounced continuously even on invincibility time (colliders always active)
    [SerializeField]
    private float invincibility;    //invincibly time after getting hit (in seconds)
    private float invTimer;
    [SerializeField]
    private float blinking;         //blinking delay (in seconds)
    private float blinkTimer;
    [SerializeField]
    [Range(0f, 1f)]
    private float blinkAlphaLow;    //lowest alpha for blinking objects
    [SerializeField]
    [Range(0f, 1f)]
    private float blinkAlphaHigh;   //highest alpha for blinking objects
    private float currentBlinkValue;
    Collider2D[] hitboxes;
    SpriteRenderer[] sprites;
    [SerializeField]
    SpriteRenderer shieldFx;
    private bool shield;

    // Set up references
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        playerController = GetComponentInParent<PlayerController>();
        hitboxes = GetComponentsInChildren<Collider2D>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Check values
        if (baseLeftMana != baseRightMana)
        {
            Debug.Log("WARNING: Left and Right Wings have different initial values! This might lead to unpredictable results with asynchrony");
        }

        if (blinkAlphaLow > blinkAlphaHigh)
        {
            float tempAlpha = blinkAlphaLow;
            blinkAlphaLow = blinkAlphaHigh;
            blinkAlphaHigh = tempAlpha;
            Debug.Log("WARNING: Alpha values are swapped and have been rearranged!");
        }
        else if (blinkAlphaLow > blinkAlphaHigh)
        {
            Debug.Log("WARNING: Alpha values low and high are the same!");
        }

        // Initialize stats
        currentLeftMana = baseLeftMana;
        currentRightMana = baseRightMana;
        invTimer = invincibility;
        currentBlinkValue = blinkAlphaLow;
        blinkTimer = 0f;
        ActivateShield(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (invTimer < invincibility)
        {
            invTimer += Time.deltaTime;
            if (invTimer >= invincibility)
            {
                ActivateHitboxes(true);
                invTimer = invincibility;
                blinkTimer = 0f;
                Debug.Log("Invincibility time has ended!");
            }
        }
    }

    // LateUpdate is called right before the render engine
    private void LateUpdate()
    {
        if (shield)                         //if we have Shield active
        {
            if (blinkTimer == 0f)           //update blinking alpha value
            {
                UpdateAlpha(shieldFx, currentBlinkValue);
            }
            blinkTimer += Time.deltaTime;
            if (blinkTimer >= blinking)     //swap blinking alpha value
            {
                if (currentBlinkValue == blinkAlphaHigh)
                {
                    currentBlinkValue = blinkAlphaLow;
                }
                else
                {
                    currentBlinkValue = blinkAlphaHigh;
                }
                blinkTimer = 0f;
            }
        }

        else if (invTimer < invincibility)  //if we are in invincibility time
        {
            if (blinkTimer == 0f)           //update blinking alpha value
            {
                foreach (SpriteRenderer sprite in sprites)
                {
                    UpdateAlpha(sprite, currentBlinkValue);
                }
            }
            blinkTimer += Time.deltaTime;
            if (blinkTimer >= blinking)     //swap blinking alpha value
            {
                if (currentBlinkValue == blinkAlphaHigh)
                {
                    currentBlinkValue = blinkAlphaLow;
                }
                else
                {
                    currentBlinkValue = blinkAlphaHigh;
                }
                blinkTimer = 0f;
            }
        }
        else    //restore alpha
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                UpdateAlpha(sprite, 1f);
            }

            //restore next blinking value to lowest
            currentBlinkValue = blinkAlphaLow;
        }
    }

    // Helper function to activate/desactivate hitboxes on all children objects
    private void ActivateHitboxes(bool activate)
    {
        if (continuousBouncing && !activate)
        {
            Debug.Log("WARNING: ContinuousBouncing is set to true and Hitboxes won't be deactivate!");
        }
        else
        {
            foreach (Collider2D hitbox in hitboxes)
            {
                if (hitbox.isTrigger)   //it only affects triggers!
                {
                    hitbox.enabled = activate;
                }
            }
        }
    }

    // Updates the Alpha component of a given sprite
    private void UpdateAlpha(SpriteRenderer sprite, float alpha)
    {
        Color c = sprite.color;
        c.a = alpha;
        sprite.color = c;
    }

    // Initialize Shield routine (WIP)
    private void ActivateShield(bool activate)
    {
        // Update Shield status
        shield = activate;

        // Reset blinking
        currentBlinkValue = blinkAlphaLow;
        blinkTimer = 0f;

        // Update sprite visibility
        shieldFx.enabled = activate;
    }

    // Starts Game Over routine (WIP)
    private void GameOver()
    {
        Debug.Log("GAME OVER");
        gameManager.LevelEndingRoutine(false);
    }

    // Updates the PlayerStats (hit) and sends the changes to the PlayerController
    // the id indicates the Wing being hit, which mana is being substracted
    public void Hit(int id, Vector2 hittingPoint)
    {
        if (invTimer >= invincibility)
        {
            if (shield) //if Shield is UP
            {
                ActivateShield(false);
                Debug.Log("Bonus SHIELD is DOWN!");
            }
            else
            {
                // Substract damage
                switch (id)
                {
                    case 0:
                        currentLeftMana -= baseDamageTaken;
                        if (currentLeftMana <= 0)
                        {
                            GameOver();
                        }
                        else if (currentLeftMana == currentRightMana)
                        {
                            ActivateShield(true);
                            Debug.Log("Bonus SHIELD is UP!");
                        }
                        break;

                    case 1:
                        currentRightMana -= baseDamageTaken;
                        if (currentRightMana <= 0)
                        {
                            GameOver();
                        }
                        else if (currentLeftMana == currentRightMana)
                        {
                            ActivateShield(true);
                            Debug.Log("Bonus SHIELD is UP!");
                        }
                        break;
                }

                // Trigger invincibility time
                ActivateHitboxes(false);
                invTimer = 0f;
                Debug.Log("Starting invincibility time!");
            }
        }

        // Push the Helioid, swap its rotation and start the control's locking time (if applicable)
        playerController.HittingRoutine(hittingPoint);
    }

    // Updates the PlayerStats (heal) and sends the changes to the PlayerController
    public void Heal(int id)
    {
        // Adds mana
        switch (id)
        {
            case 0:
                if (currentLeftMana + baseRecovery <= baseLeftMana)
                {
                    currentLeftMana += baseRecovery;
                }
                else
                {
                    currentLeftMana = baseLeftMana;
                    Debug.Log("Left Wing is overhealed!");
                }
                break;
            case 1:
                if (currentRightMana + baseRecovery <= baseRightMana)
                {
                    currentRightMana += baseRecovery;
                }
                else
                {
                    currentRightMana = baseRightMana;
                    Debug.Log("Right Wing is overhealed!");
                }
                break;
        }
    }

    // Calls the Game Manager to end the level
    public void Teleport()
    {
        gameManager.LevelEndingRoutine(true);
    }

    // Calculates the asynchrony factor between the two Wings
    // the more mana difference, the more angular asynchrony between Wings
    // asynchrony is always balanced (which means a 70% asynchrony translates into +35% -35% with independence between the specific stats - only cares about difference)
    // the theoretical maximun is 100% asynchrony (+50% -50%) corresponding the higher to the faster Angular, where 100% is the base Angular
    public float CalculateNewAsynchrony()
    {
        return Mathf.Abs((currentLeftMana - currentRightMana) / (float)(baseLeftMana + baseRightMana));
    }

    // Returns the locked time for controls after getting hit, which is always a invincibility time percentage
    public float GetLockedTime(float percent)
    {
        return Mathf.Abs(percent * invincibility);
    }

    public bool IsReady()
    {
        if (invTimer < invincibility)
        {
            return false;
        }
        return true;
    }

    // Returns the mana value of the Wing with the given ID (or -1 if error happens)
    // the id indicates the Wing being asked for
    public int GetWingMana(int id)
    {
        switch (id)
        {
            case 0:
                return currentLeftMana;
            case 1:
                return currentRightMana;
        }
        return -1;
    }
}
