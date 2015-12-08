using UnityEngine;
using System.Collections;

public class FireSpike : MonoBehaviour
{
    private PlayerController Player;
    private Vector3 startPosition;
    private int FIRESPIKE_DAMAGE, SPAWN_HEIGHT, SPEED = 20, FIRESPIKE_DISTANCE = 40;
    private float CHARGE_TIME;

	// Use this for initialization
	public void ParameteredStart (int damage, int height, float chargeTime, PlayerController player) {
        FIRESPIKE_DAMAGE = damage;
        SPAWN_HEIGHT = height;
        CHARGE_TIME = chargeTime;

	    Player = player;
	    transform.position = new Vector3(transform.position.x, height, transform.position.z);
	    startPosition = transform.position;
	}
	
	void FixedUpdate ()
	{
	    if (CHARGE_TIME <= 0)
	        MoveFirespike();
	    else
	        CHARGE_TIME -= Time.fixedDeltaTime;
	}

    private void MoveFirespike()
    {
        if(Vector3.Distance(startPosition, transform.position) < FIRESPIKE_DISTANCE)
            transform.Translate(Vector3.down * SPEED * Time.fixedDeltaTime);
        else
            DestroyFirespike();
    }

    private void DestroyFirespike()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Player.CalculateDamage(FIRESPIKE_DAMAGE);
            DestroyFirespike();
        }
    }
}
