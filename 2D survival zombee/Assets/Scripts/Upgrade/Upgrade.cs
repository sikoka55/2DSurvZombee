using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public int level;
    public int maxLevel;

    public bool isPlus;

    public float[] bonuses;

    public enum Type
    {
        speed,
        damage,
        attackSpeed,
        critDmg,
        CritChace,
        expGain

    }

    public Type type;

    public Image image;
    public TMP_Text levelText;

    public TMP_Text bonusText;
    public string bonusString;
    public void Refresh()
    {
        float levelFloat = level;
        float levelFloatMax = maxLevel;

        image.fillAmount = level / maxLevel;

        levelText.text = level + "/" + maxLevel;
            
        if(level!=maxLevel)
        {
            if (isPlus)
            {
                bonusText.text = "+ " + bonuses[level] + "% " + bonusString;
            }

            else
            {
                bonusText.text = "- " + bonuses[level] + "% " + bonusString;
            }
        }

        else
        {
            bonusText.text = "Max level";
        }
        
       
    }


}
