using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour {

    public Rigidbody body;
    public float lifetime = 4.0f;
    FireSpell source;

    public void SetSource(FireSpell spell)
    {
        source = spell;
    }

   
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(gameObject.name + " collided with " + col.gameObject.name);
        Debug.Log("fireBall hit object of type " + col.gameObject.GetType());
        if (col.gameObject.GetType() == typeof(Minion))
        {
            Debug.Log("MINION !");
            // Collide((Minion)col.gameObject);
        }
       // Collide(col.gameObject);
    }


    void Collide(Minion minion)
    {
        Debug.Log("FIRE HIT ");
        source.notifyHit(minion);
    }

    void Collide(GameObject gameObject)
    {
        Debug.Log("other object...");
    }

    void Awake()
    {
        Destroy(gameObject, lifetime);
    }
}
