using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private GUIManager Gui;
    private CharacterController Character;
    private PlayerSword Sword;
    private PlayerShield Shield;
    private Vector3 eulers;
    public float Health, Stamina, Mana, SprintSpeed, Speed, Gravity, Rotation;
    private float xRot = 0, yRot = 0, downAcceleration, speedTemp, invincibilityTime = 0;
    private float MAX_STAMINA, MAX_MANA, MAX_HEALTH;
    private readonly int INVINCIBILITY_TIME = 1, STAMINA_RESOTRATION = 10, MANA_RESTORATION = 10;

    // Use this for initialization
    void Start()
    {
        Character = GetComponent<CharacterController>();
        Gui = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GUIManager>();
        Sword = GameObject.FindGameObjectWithTag("RunicAxe").GetComponent<PlayerSword>();
        Shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<PlayerShield>();

        speedTemp = Speed;
        downAcceleration = -Gravity;
        eulers = transform.eulerAngles;

        MAX_HEALTH = Health;
        MAX_STAMINA = Stamina;
        MAX_MANA = Mana;
    }

    // Update is called once per frame
    void Update()
    {
        Gui.UpdatePlayerHealth((int)Health);
        Gui.UpdatePlayerMana((int)Mana);
        Gui.UpdatePlayerStamina((int)Stamina);

        if (Health <= 0)
            Die();

        if (Stamina < MAX_STAMINA)
            RestoreStamina(STAMINA_RESOTRATION);

        if(Mana < MAX_MANA)
            RestoreMana(MANA_RESTORATION);

        if (invincibilityTime >= 0)
            invincibilityTime -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        MoveCharacter();
        RotateCharacter();

        if (Character.isGrounded)
            downAcceleration = -Gravity;

        if (!Character.isGrounded)
            downAcceleration -= Gravity;

        Character.Move(new Vector3(0, downAcceleration, 0) * Time.fixedDeltaTime);
    }

    void MoveCharacter()
    {
        Speed = speedTemp;
        if (Input.GetKey(KeyCode.LeftShift))
            Speed = SprintSpeed;


        if (Input.GetButton("Vertical"))
            Character.Move(transform.forward * Speed * Time.fixedDeltaTime * Input.GetAxis("Vertical"));
        if (Input.GetButton("Horizontal"))
            Character.Move(transform.right * Speed * Time.fixedDeltaTime * Input.GetAxis("Horizontal"));
    }

    void RotateCharacter()
    {
        eulers.x -= Input.GetAxis("Mouse Y") * Rotation * Time.fixedDeltaTime;
        eulers.y += Input.GetAxis("Mouse X") * Rotation * Time.fixedDeltaTime;
        eulers.z = 0;

        transform.eulerAngles = eulers;
    }

    public void CalculateDamage(int damage)
    {
        damage = Shield.TryBlockAttack(Stamina, damage);
        if (invincibilityTime <= 0)
        {
            ApplyDamage(damage);
            invincibilityTime = INVINCIBILITY_TIME;
        }
    }

    private void ApplyDamage(int damage)
    {
        Health -= damage;
    }

    public void RemoveStamina(int staminaCost)
    {
        Stamina -= staminaCost;
    }

    public void RemoveMana(int manaCost)
    {
        Mana -= manaCost;
    }

    private void RestoreStamina(float newStamina)
    {
        newStamina = newStamina * Time.deltaTime;
        if (Stamina < MAX_STAMINA)
        {
            if ((newStamina + Stamina) > MAX_STAMINA)
                Stamina = MAX_STAMINA;
            else
                Stamina += newStamina;
        }
    }

    private void RestoreMana(float newMana)
    {
        newMana = newMana*Time.deltaTime;
        if (Mana < MAX_MANA)
        {
            if ((newMana + Mana) > MAX_MANA)
                Mana = MAX_MANA;
            else
                Mana += newMana;
        }
    }

    void Die()
    {

    }
}
