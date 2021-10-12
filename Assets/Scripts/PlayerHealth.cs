using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currHealth;
    public UIHealthBar HPBar;
    public DeathMenu deathMenu;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        HPBar.SetHealthMax(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //DEBUG function: damage player w/ Right Shift key
        if (Input.GetKeyDown(KeyCode.RightShift)) {
            modifyHealth(-10);
        }

        //Update Invincibility Time (if applicable)
        if (isInvincible == true)
        {
            invincibleTimer -= Time.deltaTime;
            Debug.Log("Timer Active");
            
            //Disable Timer
            if (invincibleTimer < 0)
            {
                isInvincible = false;
                Debug.Log("Timer Disabled");
            }
        }
    }

    public void modifyHealth(int healthMod) 
    {
        if (healthMod < 0)
        {
            //Block Damage while invincibility timer is active
            if (isInvincible == true)
            {
                return;
            }

            //Enable Invincibility Timer after taking damage
            isInvincible = true;
            invincibleTimer = timeInvincible;
            Debug.Log("Timer Enabled");
        }

        currHealth = currHealth + healthMod;
        HPBar.SetHealth(currHealth);

        if (currHealth <= 0) 
        {
            deathMenu.gamePause();
        }
    }
}
