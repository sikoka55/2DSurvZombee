using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;

    [Header("Objects")]
    public Transform target;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public Image healthImg;
    public Image EXPImg;

    [Header("Stats")]
    public float speed;
    public float speedRotation;

    public float healthMax;
    public float health;
    public float exp;
    public float exp_max;

    public bool isAtacked;


    [Header("Shooting")]
    public float timer;
    public float timerMax;
    public float damage;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Animating();
        Health();
        Moving_Rotating();
        Shooting();
    }



        private void Moving_Rotating()
    {
        transform.position = new Vector3(transform.position.x + SimpleInput.GetAxis("Horizontal2") * speed,
                                         transform.position.y + SimpleInput.GetAxis("Vertical2") * speed, 0);

        float rotationInputX = SimpleInput.GetAxis("Horizontal2");
        float rotationInputY = SimpleInput.GetAxis("Vertical2");

        if (Mathf.Abs(rotationInputX) > 0.1f || Mathf.Abs(rotationInputY) > 0.1f)
        {
            float rotationAngle = Mathf.Atan2(rotationInputY, rotationInputX) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle);
            transform.rotation = rotation;
        }

    }

    private void Shooting()
    {
        if (timer > 0)
        {
            timer--;

        }
        else
        {
            timer = timerMax;

            GameObject newBullet = Instantiate(bulletPrefab, shootPoint.transform.position, Quaternion.identity, null);
            newBullet.GetComponent<Bullet>().target = shootPoint.transform;
            newBullet.GetComponent<Bullet>().damage = damage;

        }
        
     }

    public void Damage(float damage)
    {
        health -= damage;
        


        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Health()
    {
        healthImg.fillAmount = health / healthMax;
    }

    public void Animating()
    {
        if (isAtacked)
        {
            animator.SetBool("isAtacked", true);
        }
        else
        {
            animator.SetBool("isAtacked", false);
        }
        
    }
 }

