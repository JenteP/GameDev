using UnityEngine;
using System.Collections;

public class FireBubble : MonoBehaviour
{
    private PlayerController Player;
    private Vector3 startPosition;
    private int FIREBUBBLE_DAMAGE, SPEED = 10; 
    private double FIREBUBBLE_LIFETIME;

    // Use this for initialization
    public void ParameteredStart(int damage, double lifetime, PlayerController player)
    {
        FIREBUBBLE_DAMAGE = damage;
        FIREBUBBLE_LIFETIME = lifetime;
        Player = player;

        transform.position = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveFirebubble();    
    }

    private void MoveFirebubble()
    {
        if (FIREBUBBLE_LIFETIME > 0)
        {
            transform.LookAt(Player.transform);
            transform.Translate(Vector3.forward * SPEED * Time.fixedDeltaTime);
            FIREBUBBLE_LIFETIME -= Time.fixedDeltaTime;
        }
        else
            DestroyFirebubble();
    }

    private void DestroyFirebubble()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Player.CalculateDamage(FIREBUBBLE_DAMAGE);
            DestroyFirebubble();
        }
    }
}
