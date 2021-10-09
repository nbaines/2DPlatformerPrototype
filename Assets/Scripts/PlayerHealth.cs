using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currHealth;
    public UIHealthBar HPBar;

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
    }

    void modifyHealth(int healthMod) {
        currHealth = currHealth + healthMod;
        HPBar.SetHealth(currHealth);
    }
}
