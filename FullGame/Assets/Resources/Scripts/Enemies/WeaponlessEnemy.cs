using System;
using UnityEngine;
using System.Collections;

public class WeaponlessEnemy : Enemy
{
    private double AttackTimer = 2;
    private double immunityTimer = 2;
    private readonly double MIN_MELEE_DISTANCE = 5;
    private readonly double ATTACK_TIMER = 2, INVINCIBILITY_TIME = 1;
    private readonly double ANIMATION_TIMER = 0.9;

    // Update is called once per frame
    void Update()
    {
        if (!IsDead)
        {
            if (IsHit)
            {
                InvincibilityTime = INVINCIBILITY_TIME;
                IsHit = false;
            }
            else
                InvincibilityTime -= Time.deltaTime;

            if (Health <= 0)
            {
                if (!IsDead)
                {
                    IsDead = true;
                    IsPlayerInRange = false;
                    Destroy(GetComponent<CapsuleCollider>());
                    Destroy(EnemyBody);
                    StartCoroutine(Die());
                }
            }    
        }
    }

    void FixedUpdate()
    {
        if (!IsDead)
        {
            if (Vector3.Distance(Player.transform.position, transform.position) < MIN_MELEE_DISTANCE)
                Attack();
            else
                AttackMove();    
        }
    }

    public override void ApplyDamage(int damage)
    {
        Health -= damage;
        GuiManager.UpdateEnemyHealth(this, Health);
    }

    public override void AttackMove()
    {
        AttackTimer = 1;
        EnemyAnimator.SetBool("Run", true);
        EnemyAnimator.SetBool("Attack", false);
        Vector3 playerPosition = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
        transform.LookAt(playerPosition);

        EnemyBody.MovePosition(transform.position + (transform.forward * Speed * Time.fixedDeltaTime));
    }

    public override void Attack()
    {
        EnemyAnimator.SetBool("Run", false);
        if (AttackTimer <= 0)
        {
            EnemyAnimator.SetBool("Attack", true);
            Player.CalculateDamage(10);
            AttackTimer = ATTACK_TIMER;
        }
        else
        {
            EnemyAnimator.SetBool("Attack", false);
            AttackTimer -= Time.fixedDeltaTime;
        }
    }

    public override void SpecialAttack()
    {
        //weaponless enemy has no special
    }

    public override IEnumerator Die()
    {
        EnemyAnimator.SetBool("Run", false);
        EnemyAnimator.SetBool("Attack", false);
        EnemyAnimator.SetBool("Die", true);
        do
        {
            DeathAnimationTimer -= Time.deltaTime;
            yield return null;
        } while (DeathAnimationTimer > 0);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("RunicAxe"))
        {
            if (InvincibilityTime <= 0)
            {
                PlayerSword playerSword = collision.gameObject.GetComponent<PlayerSword>();
                playerSword.AddEnemyToRange(this);
                IsHit = true;
            }
        }
    }
}

