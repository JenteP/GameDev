using UnityEngine;
using System.Collections;

public class TriggerManager : MonoBehaviour {
    private SpawnManager spawner;

	// Use this for initialization
	void Start () {
	    spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpawnManager>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            string name = gameObject.name;
            switch (name)
            {
                case "Trigger_1": spawner.SpawnEnemyGroup1();
                    break;
            }
        }
    }
}
