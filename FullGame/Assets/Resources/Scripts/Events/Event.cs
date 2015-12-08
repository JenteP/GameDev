using UnityEngine;
using System.Collections;

public abstract class Event : MonoBehaviour
{

    protected GameObject Trigger;

    protected void Start()
    {
        Trigger = gameObject;
    }

    protected abstract void ResolveEvent();

    protected void OnTriggerEnter(Collider other)
    {
        ResolveEvent();
    }

    protected void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }
}
