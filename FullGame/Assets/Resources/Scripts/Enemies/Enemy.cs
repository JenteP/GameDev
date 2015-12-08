using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    protected float Speed = 10;
    protected float RunSpeed = 30;
    protected int Health = 100;
    protected int Damage = 20;
    protected bool IsPlayerInRange;
    protected PlayerController Player;
    protected Rigidbody EnemyBody;
    protected Animator EnemyAnimator;
    protected GUIManager GuiManager;
    protected double DeathAnimationTimer = 60.0, InvincibilityTime = 1;
    protected bool IsDead = false, IsHit;

    protected void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        GuiManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GUIManager>();
        EnemyBody = gameObject.GetComponent<Rigidbody>();
        EnemyAnimator = gameObject.GetComponent<Animator>();
    }

    public abstract void ApplyDamage(int damage);
    public abstract void AttackMove();
    public abstract void Attack();
    public abstract void SpecialAttack();
    public abstract IEnumerator Die();
}
