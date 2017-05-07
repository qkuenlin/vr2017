using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
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


    virtual protected void OnTriggerEnter(Collider col)
    {
        Minion minion = (Minion)col.GetComponent("Minion");
        if (minion != null)
        {
            Collide(minion);
            Destroy(gameObject);
        }
    }


    virtual protected void SpecialEffect()//explostion, burn, etc. must be defined in the actual spells

    {

    }


    protected void Collide(Minion minion)

    {
        source.notifyHit(minion);
        SpecialEffect();
    }
}
