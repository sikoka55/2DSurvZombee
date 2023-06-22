using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgController : MonoBehaviour
{
    public Upgrade[] upgrades;

    public Player player;
    public Bullet bullet;
    public Enemy_Health enemy;

    private void Start()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].Refresh();
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for (int i =0; i < upgrades.Length; i++)
            {
                upgrades[i].Refresh();
            }
        }
    }

    public void Upgrade(int index)
    {
        if (index == 0)
        {

            float bonus = player.damage / 100 * upgrades[index].bonuses[upgrades[index].level];
            player.damage += bonus;

        }

        else if (index == 1)
        {
            float bonus = player.speed / 100 * upgrades[index].bonuses[upgrades[index].level];
            player.speed += bonus;
        }

        else if (index == 2)
        {
            float bonus = player.timerMax / 100 * upgrades[index].bonuses[upgrades[index].level];
            player.timerMax -= bonus;
        }

        else if (index == 3)
        {
            float bonus = player.criticalRate / 100 * upgrades[index].bonuses[upgrades[index].level];
            player.criticalRate += bonus;
        }

        else if (index == 4)
        {
            float bonus = player.criticalChance / 100 * upgrades[index].bonuses[upgrades[index].level];
            player.criticalChance += bonus;
        }

        else if (index == 5)
        {
            float bonus = player.expGain / 100 * upgrades[index].bonuses[upgrades[index].level];
            player.expGain += bonus;
        }

        upgrades[index].level++;

        if (upgrades[index].level == upgrades[index].maxLevel)
        {
            upgrades[index].GetComponent<Button>().interactable = false;
        }

        
        
        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].Refresh();
        }

        gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    public void ShuffleAndShowUpgrades()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        // Create a list to store available upgrades
        List<Upgrade> availableUpgrades = new List<Upgrade>();

        // Find available upgrades (level < maxLevel and not active)
        foreach (Upgrade upgrade in upgrades)
        {
            if (upgrade.level < upgrade.maxLevel && !upgrade.gameObject.activeSelf)
            {
                availableUpgrades.Add(upgrade);
            }
        }

        // Shuffle the available upgrades
        availableUpgrades.Shuffle();

        // Show the first three shuffled upgrades
        int numberToShow = Mathf.Min(3, availableUpgrades.Count);
        for (int i = 0; i < numberToShow; i++)
        {
            Upgrade upgrade = availableUpgrades[i];
            upgrade.gameObject.SetActive(true);
        }
    }


}
