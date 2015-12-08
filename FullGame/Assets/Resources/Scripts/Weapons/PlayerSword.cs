using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerSword : MonoBehaviour
{
    private Animator AxeAnimator;
    private PlayerController Player;
    private readonly double ANIMATION_TIMER = 0.9;
    private double animationTimer = 0.9;
    private readonly int HEAVY_ATTACK_DAMAGE = 30, NORMAL_ATTACK_DAMAGE = 10, ATTACK_STAMINA_REDUCTION = 10, HEAVY_ATTACK_STAMINA_REDUCTION = 25;
    private IList<Enemy> enemiesInCollider;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        AxeAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTimer == ANIMATION_TIMER)
        {
            if (Player.Stamina >= ATTACK_STAMINA_REDUCTION)
            {
                if (Input.GetButtonUp("Fire1"))
                {
                    AxeAnimator.SetBool("Attack", true);
                    Player.RemoveStamina(ATTACK_STAMINA_REDUCTION);
                    StartCoroutine(WaitForAnimation("Attack", NORMAL_ATTACK_DAMAGE));
                    SetNewEnemiesInRange();
                }
            }

            if (Player.Stamina >= HEAVY_ATTACK_STAMINA_REDUCTION)
            {
                if (Input.GetButtonUp("Fire2"))
                {
                    AxeAnimator.SetBool("HeavyAttack", true);
                    Player.RemoveStamina(HEAVY_ATTACK_STAMINA_REDUCTION);
                    StartCoroutine(WaitForAnimation("HeavyAttack", HEAVY_ATTACK_DAMAGE));
                    SetNewEnemiesInRange();
                }     
            }   
        }
    }

    //Wait 1 second before going back to the idle state to finish the (heavy) attack animation
    IEnumerator WaitForAnimation(String animationId,int damage)
    {
        do
        {
            animationTimer -= Time.deltaTime;
            yield return null;
        } while (animationTimer > 0);

        AxeAnimator.SetBool(animationId, false);
        animationTimer = ANIMATION_TIMER; //reset timer
        foreach (var enemy in enemiesInCollider)
        {
            enemy.ApplyDamage(damage);
        }
        enemiesInCollider.Clear();
    }

    private void SetNewEnemiesInRange()
    {
        if (enemiesInCollider == null)
            enemiesInCollider = new List<Enemy>();
    }

    public void AddEnemyToRange(Enemy enemy)
    {
        if (enemiesInCollider == null)
            enemiesInCollider = new List<Enemy>();

        enemiesInCollider.Add(enemy);
    }
}
