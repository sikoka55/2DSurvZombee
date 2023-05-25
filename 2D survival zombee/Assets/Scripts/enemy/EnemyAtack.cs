using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    public float damage;
    public float damageTimer;
    private bool canAtack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null && canAtack)
        {
            player.isAtacked = true;
            player.Damage(damage);
            StartCoroutine(EnemyAtackTimer(player));

        }
    }
    

    private IEnumerator EnemyAtackTimer (Player player)
    {
        canAtack = false;
        yield return new WaitForSeconds(damageTimer);
        player.isAtacked = false;
        canAtack = true;
        
    }
}
