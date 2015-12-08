using UnityEngine;
using System.Collections;

public class CasterEnemy : Enemy
{
    private EnemyMagic Magic;
    private double AttackTimer = 2, immunityTimer = 2, specialTimer = 2, specialTemp = 0, firespikeCastTemp, firebubbleCastTemp = 1;
    private double FIRESPIKE_CAST_TIME, INVINCIBILITY_TIME = 1, MAX_CASTING_DISTANCE = 45,
        SPECIAL_RECHARGE_TIMER = 10, SPECIAL_TIME = 1, FIREBUBBLE_CAST_TIME = 1; 

    void Awake()
    {
        firespikeCastTemp = Random.Range(2, 8);
        FIRESPIKE_CAST_TIME = firespikeCastTemp;
        Magic = GetComponent<EnemyMagic>();
    }
        
	// Update is called once per frame
	void Update () {
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

            if(distance < MAX_CASTING_DISTANCE && specialTimer >= 0)
                SpecialAttack();
            else if (distance < MAX_CASTING_DISTANCE)
                Attack();
            else if(distance > MAX_CASTING_DISTANCE)
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
        EnemyAnimator.SetBool("Run", true);
        EnemyAnimator.SetBool("Attack", false);
        Vector3 playerPosition = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
        transform.LookAt(playerPosition);

        EnemyBody.MovePosition(transform.position + (transform.forward * Speed * Time.fixedDeltaTime));
    }

    public override void Attack()
    {
        EnemyAnimator.SetBool("Run", false);
        if (FIRESPIKE_CAST_TIME <= 0)
        {
            EnemyAnimator.SetBool("Attack", true);
            Magic.CastFirespike();
            FIRESPIKE_CAST_TIME = firespikeCastTemp;
        }
        else
            FIRESPIKE_CAST_TIME -= Time.fixedDeltaTime;
    }

    public override void SpecialAttack()
    {
        EnemyAnimator.SetBool("Run", false);
        if (FIREBUBBLE_CAST_TIME <= 0)
        {
            EnemyAnimator.SetBool("Attack", true);
            Magic.CastFirebubble();
            FIREBUBBLE_CAST_TIME = firebubbleCastTemp;
        }
        else
            FIREBUBBLE_CAST_TIME -= Time.fixedDeltaTime;
        specialTimer -= Time.fixedDeltaTime;
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

    private void OnCollisionEnter(Collision collision)
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
