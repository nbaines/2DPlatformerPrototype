using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Slider HPSlider;

    //Use: Changes player's maximum HP (on HP bar)
    public void SetHealthMax(int maxHP)
    {
        HPSlider.maxValue = maxHP; //set HP bar's max value to new max HP
        HPSlider.value = maxHP; //fill HP bar to new max HP
    }

    //Use: Changes player's current HP (on HP bar)
    public void SetHealth(int HP)
    {
        HPSlider.value = HP;
    }
}
