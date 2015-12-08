using UnityEngine;
using System.Collections;
using Debug = System.Diagnostics.Debug;

public class ArmedEnemy : Enemy
{
    private double AttackTimer = 2, immunityTimer = 2, specialTimer = 2, specialTemp = 0;
    private readonly int SPECIAL_ATTACK_DISTANCE = 40;
    private readonly float DASH_ATTACK_SPEED_MULTIPLIER = 3;
    private readonly double ATTACK_TIMER = 2, INVINCIBILITY_TIME = 1, MIN_MELEE_DISTANCE = 5, SPECIAL_RECHARGE_TIMER = 30, SPECIAL_TIME = 1.5;

    // Update is called once per frame
    void Update()
    {
        if (!IsDead)
        {
            if (specialTimer <= 0)
            {
                specialTemp += Time.deltaTime;
                if (specialTemp >= SPECIAL_RECHARGE_TIMER)
                {
                    specialTimer = SPECIAL_TIME;
                    specialTemp = 0;
                }
            }

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
            float distance = Vector3.Distance(Player.transform.position, transform.position);

            if (distance <= SPECIAL_ATTACK_DISTANCE && specialTimer >= 0)
                SpecialAttack();
            else if (distance <= MIN_MELEE_DISTANCE)
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
        float dashSpeed = Speed * DASH_ATTACK_SPEED_MULTIPLIER;
        EnemyBody.MovePosition(transform.position + (transform.forward * dashSpeed * Time.fixedDeltaTime));
        specialTimer -= Time.fixedDeltaTime;

        if(Vector3.Distance(Player.transform.position, transform.position) <= MIN_MELEE_DISTANCE)
            Player.CalculateDamage(15);
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
