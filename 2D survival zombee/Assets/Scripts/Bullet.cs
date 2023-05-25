using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float shootForce;

    public float damage;

    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = target.rotation;

        Vector3 direction = initialRotation * Vector3.right;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * shootForce;

        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        // Встановлення кута повороту кулі відповідно до початкового кута персонажа
        transform.rotation = initialRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Enemy_Health>())
        {
            collision.gameObject.GetComponent<Enemy_Health>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
