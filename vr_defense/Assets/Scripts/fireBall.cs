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

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(gameObject.name + " collided with " + col.gameObject.name);
        Collide(col.gameObject);
    }

    void Collide(Minion minion)
    {
        Debug.Log("FIRE HIT ");
        //wizard.GiveXP(minion.Hit(SpellDamage(), this));
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
