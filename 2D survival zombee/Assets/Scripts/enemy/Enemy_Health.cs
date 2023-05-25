using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public float health;
    public float exp;
    private Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Damage (float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
            player.ExpChange(exp);
        }

        
    }
}
