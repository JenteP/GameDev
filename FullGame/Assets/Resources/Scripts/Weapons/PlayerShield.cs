using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour
{
    public Animator ShieldAnimator;
    public PlayerController Player;
    private readonly double SET_SHIELD_TIMER = 5;
    private double setShieldTimer = 5;
    private bool isBlocking;
    private readonly int BLOCK_STAMINA_REDUCTION = 15;
    

	// Use this for initialization
	void Start ()
	{
	    ShieldAnimator = GetComponent<Animator>();
	    Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    void Update()
    {
        if (Player.Stamina > BLOCK_STAMINA_REDUCTION)
        {
            if (Input.GetKey(KeyCode.C))
            {
                ShieldAnimator.SetBool("Block", true);
                StartCoroutine(HoldShield());
            }
            else
            {
                ShieldAnimator.speed = 1;
                setShieldTimer = SET_SHIELD_TIMER;
                isBlocking = false;
                ShieldAnimator.SetBool("Block", false);
            }      
        }
        else
        {
            ShieldAnimator.speed = 1;
            ShieldAnimator.SetBool("Block", false);
        }
    }
	
    IEnumerator HoldShield()    
    {
        while (setShieldTimer > 0)
        {
            setShieldTimer -= Time.deltaTime;
            yield return null;
        }
        isBlocking = true;
        ShieldAnimator.speed = 0;
    }

    public int TryBlockAttack(float stamina, int damage)
    {
        if (isBlocking && stamina > BLOCK_STAMINA_REDUCTION)
        {
            Player.RemoveStamina(BLOCK_STAMINA_REDUCTION);
            return 0; //Damage blocked
        }
        return damage;
    }
}
