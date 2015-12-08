using UnityEngine;
using System.Collections;

public class EnemyMagic : MonoBehaviour {
    private PlayerController Player;
    private GameObject FirebubblePrefab, FirespikePrefab;
    private readonly int FIRESPIKE_DAMAGE = 20, FIRESPIKE_HEIGHT = 20, FIRESPIKE_CHARGE_TIMER = 2;
    private readonly int FIREBUBBLE_DAMAGE = 20, FIREBUBBLE_LIFETIME = 5;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        FirespikePrefab = Resources.Load("Magic/Firespike") as GameObject;
        FirebubblePrefab = Resources.Load("Magic/Firebubble") as GameObject;
	}

    public void CastFirespike()
    {
        GameObject firespikePrefab = Instantiate(FirespikePrefab, Player.transform.position, Quaternion.identity) as GameObject;
        FireSpike firespike = firespikePrefab.GetComponent<FireSpike>();
        firespike.ParameteredStart(FIRESPIKE_DAMAGE, FIRESPIKE_HEIGHT, FIRESPIKE_CHARGE_TIMER, Player);
    }

    public void CastFirebubble()
    {
        GameObject firebubblePrefab = Instantiate(FirebubblePrefab, transform.position, Quaternion.identity) as GameObject;
        FireBubble firespike = firebubblePrefab.GetComponent<FireBubble>();
        firespike.ParameteredStart(FIREBUBBLE_DAMAGE, FIREBUBBLE_LIFETIME, Player);
    }
}
