using UnityEngine;
using System.Collections;

public class FireBurst : MonoBehaviour
{
    private PlayerController Player;
    private float spawnDistance = 10;
    private Vector3 startPosition;
    private Vector3 direction;
    private readonly int SPEED = 40;
    private int FIREBURST_DAMAGE, MAX_FIREWAVE_DISTANCE;

    // Use this for initialization
    public void ParameteredStart(int damage, int maxDistance, PlayerController player) //other name to init with parameters
    {
        FIREBURST_DAMAGE = damage;
        MAX_FIREWAVE_DISTANCE = maxDistance;

        Player = player;
        direction = Player.transform.forward;

        transform.position = Player.transform.position + direction * spawnDistance;
        startPosition = Player.transform.position + direction * spawnDistance;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveFireburst();
    }

    private void MoveFireburst()
    {
        float distance = Vector3.Distance(startPosition, transform.position);

        if (distance <= MAX_FIREWAVE_DISTANCE)
        {
            transform.Translate(direction * SPEED * Time.fixedDeltaTime);
        }
        else
        {
            DestroyFireburst();
        }
    }

    private void DestroyFireburst()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.ApplyDamage(FIREBURST_DAMAGE);
        }
    }
}
