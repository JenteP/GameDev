using UnityEngine;
using System.Collections;

public class PlayerMagic : MonoBehaviour
{
    private PlayerController Player;
    private GameObject FireballPrefab, FireburstPrefab;
    private readonly int FIREBALL_DAMAGE = 50, FIREBURST_DAMAGE = 80;
    private readonly int MAX_FIREBALL_DISTANCE = 60, MAX_FIREBURST_DISTANCE = 80;
    private readonly int FIREBALL_MANA_COST = 30, FIREBURST_MANA_COST = 90;

	// Use this for initialization
	void Start ()
	{
	    Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	    FireballPrefab = Resources.Load("Magic/Fireball") as GameObject;
        FireburstPrefab = Resources.Load("Magic/Fireburst") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyUp(KeyCode.E))
	    {
	        if (Player.Mana >= FIREBALL_MANA_COST)
	        {
                CastFireball();
                Player.RemoveMana(FIREBALL_MANA_COST);
	        }
	    }

	    if (Input.GetKeyUp(KeyCode.F))
	    {
	        if (Player.Mana >= FIREBURST_MANA_COST)
	        {
                CastFireBurst();
                Player.RemoveMana(FIREBURST_MANA_COST);
	        }
	    }
	}

    void CastFireball()
    {
        GameObject fireballInstance = Instantiate(FireballPrefab, transform.position, Quaternion.identity) as GameObject;
        FireBall fireball = fireballInstance.GetComponent<FireBall>();
        fireball.ParameteredStart(FIREBALL_DAMAGE, MAX_FIREBALL_DISTANCE, Player);
    }

    void CastFireBurst()
    {
        GameObject fireburstInstance = Instantiate(FireburstPrefab, transform.position, Quaternion.identity) as GameObject;
        FireBurst fireburst = fireburstInstance.GetComponent<FireBurst>();
        fireburst.ParameteredStart(FIREBURST_DAMAGE, MAX_FIREBURST_DISTANCE, Player);
    }
}
