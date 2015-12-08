using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour
{
    private PlayerController Player;
    private float spawnDistance = 10;
    private Vector3 startPosition;
    private Vector3 direction;
    private readonly int SPEED = 60;
    private int FIREBALL_DAMAGE, MAX_FIREBALL_DISTANCE;

    public void ParameteredStart(int damage, int maxDistance, PlayerController player)
    {
        FIREBALL_DAMAGE = damage;
        MAX_FIREBALL_DISTANCE = maxDistance;

        Player = player;
        direction = Player.transform.forward;

        transform.position = Player.transform.position + direction*spawnDistance;
        startPosition = Player.transform.position + direction * spawnDistance;
    }

    void FixedUpdate()
    {
        MoveFireball();   
    }

    private void MoveFireball()
    {
        float distance = Vector3.Distance(startPosition, transform.position);

        if (distance <= MAX_FIREBALL_DISTANCE)
        {
            transform.Translate(direction * SPEED * Time.fixedDeltaTime);    
        }
        else
        {
            DestroyFireball();
        }
    }

    private void DestroyFireball()
    {
        Destroy(gameObject);    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.ApplyDamage(FIREBALL_DAMAGE);
        }
    }
}
