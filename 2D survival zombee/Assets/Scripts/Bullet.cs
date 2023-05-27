using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    SpriteRenderer sprite;
    public Transform target;
    public float shootForce;

    [Range(0, 100)]
    public float criticalHitChance;
    public float criticalHitRate;
    public float damage;




    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();


        Vector3 direction = target.rotation * Vector3.right;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * shootForce;

        if (Random.Range(0, 100) < criticalHitChance)
        {
            damage *= criticalHitRate;
            sprite.color = Color.red;
        }

        Destroy(gameObject, 3f);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {



        if (collision.gameObject.GetComponent<Enemy_Health>())
        {

            collision.gameObject.GetComponent<Enemy_Health>().Damage(damage);

            Destroy(gameObject);

        }
    }
}