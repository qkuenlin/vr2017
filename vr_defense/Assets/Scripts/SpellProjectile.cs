using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour {
    public Spell source;
    public Rigidbody body;

    public void SetSource(Spell spell)
    {
        source = spell;
    }

    public string SpellType()
    {
        return source.SpellType();
    }

    public float Damage()
    {
        return source.Damage();
    }

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(gameObject.name + " collided with " + col.gameObject.name);

        Minion minion = (Minion)col.GetComponent("Minion");
        if (minion != null)
        {
            Collide(minion);
            Destroy(gameObject);
        }
    }

    void SpecialEffect()//explostion, burn, etc. must be defined in the actual spells
    {

    }

    void Collide(Minion minion)
    {
        source.notifyHit(minion);
        SpecialEffect();
    }
}
