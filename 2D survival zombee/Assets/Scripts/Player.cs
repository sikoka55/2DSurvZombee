using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private UpgController upg;

    [Header("Objects")]
    public Transform target;
    public GameObject bulletPrefab;
    public Transform shootPoint;

    public Image healthImg;
    public Image EXPImg;
    public TMP_Text curLevelText;

    public GameObject upgradePanel;

    [Header("Stats")]
    public float speed;
    public float speedRotation;

    public float healthMax;
    public float health;

    public float exp;
    public float exp_max;
    public float expCoef;
    public float expUpgrade;
    public int level;
    

    public bool isAttacked;


    [Header("Shooting")]
    public float timer;
    public float timerMax;
    public float damage;
    public float criticalChance;
    public float criticalRate;

    private void Start()
    {
        UIRefresh();
        animator = GetComponent<Animator>();
        upg = upgradePanel.GetComponent<UpgController>(); 
    }



    private void FixedUpdate()
    {
        Animating();
        Moving();
        Shooting();
        Rotating();
        Shooting();
    }



    private void Moving()
    {
        transform.position = new Vector3(transform.position.x + SimpleInput.GetAxis("Horizontal2") * speed,
                                         transform.position.y + SimpleInput.GetAxis("Vertical2") * speed, 0);

    }

    private void Rotating()
    {


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
            

            GameObject newBullet = Instantiate(bulletPrefab, shootPoint.transform.position, transform.rotation, null);
            newBullet.GetComponent<Bullet>().target = shootPoint;
            newBullet.GetComponent<Bullet>().damage = damage;

            //fix for buttns. Cntrl+C from bullet to Player
            newBullet.GetComponent<Bullet>().criticalHitChance = criticalChance;
            newBullet.GetComponent<Bullet>().criticalHitChance = criticalRate;
        }

    } 
     

    public void Damage(float damage)
    {
        health -= damage;
        


        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }

        UIRefresh();
    }



    public void Animating()
    {
        if (isAttacked)
        {
            animator.SetBool("isAtacked", true);
        }
        else
        {
            animator.SetBool("isAtacked", false);
        }
        
    }

    public void UIRefresh()
    {
        exp = Mathf.Clamp(exp, 0, exp_max);
        health = Mathf.Clamp(health, 0, healthMax);

        healthImg.fillAmount = health / healthMax;
        EXPImg.fillAmount = exp / exp_max;

        curLevelText.text = level.ToString();
    }

    public void ExpChange(float count)
    {
        exp += count;

        if (exp >= exp_max)
        {
            level++;

            exp = 0;
            exp_max *= expCoef;

            Time.timeScale = 0;
            upgradePanel.SetActive(true);
            upg.ShuffleAndShowUpgrades();
            
        }

        UIRefresh();
    }
 }

