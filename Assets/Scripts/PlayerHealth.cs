using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currHealth;
    public UIHealthBar HPBar;
    public DeathMenu deathMenu;
    public AudioSource audioS;
    public AudioClip clip;
    public PlayerController controller;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    private Material materialDefault;
    private Material materialDamage;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        HPBar.SetHealthMax(maxHealth);
        audioS = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        materialDefault = spriteRenderer.material;
        materialDamage = Resources.Load("DamageRed", typeof(Material)) as Material;
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        //DEBUG function: damage player w/ Right Shift key
        ////if (Input.GetKeyDown(KeyCode.RightShift)) {
        ////    modifyHealth(-10);
        ////}

        //Update Invincibility Time (if applicable)
        if (isInvincible == true)
        {
            invincibleTimer -= Time.deltaTime;
            
            //Disable Timer
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
    }

    public void modifyHealth(int healthMod) 
    {
        float healthOnCall = currHealth;
        if (healthMod < 0)
        {
            //Block Damage while invincibility timer is active
            if (isInvincible == true)
            {
                return;
            }

            //Flash sprite ("damaged" indicator)
            damageShader();
            Invoke("resetShader", 0.1f);
            Invoke("damageShader", 0.2f);
            Invoke("resetShader", 0.3f);

            //Enable Invincibility Timer after taking damage
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        
        currHealth = Mathf.Clamp(currHealth + healthMod, 0, maxHealth);
        if (currHealth < healthOnCall)  //check if we took damage from this function, if so, play a sound
            controller.PlayDamage();
        
        HPBar.SetHealth(currHealth);

        if (currHealth <= 0) 
        {
            deathMenu.gamePause();
        }
    }

    //Changes the sprite material/shader to red (damaged)
    private void damageShader()
    {
        spriteRenderer.material = materialDamage;
    }

    //Reverts the sprite material/shader to the default one
    private void resetShader() {
        spriteRenderer.material = materialDefault;
    }
}
