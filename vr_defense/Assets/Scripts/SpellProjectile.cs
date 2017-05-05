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

<<<<<<< HEAD
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(gameObject.name + " collided with " + col.gameObject.name);

=======
    virtual protected void OnTriggerEnter(Collider col)
    {
>>>>>>> master
        Minion minion = (Minion)col.GetComponent("Minion");
        if (minion != null)
        {
            Collide(minion);
            Destroy(gameObject);
        }
    }

<<<<<<< HEAD
    void SpecialEffect()//explostion, burn, etc. must be defined in the actual spells
=======
    virtual protected void SpecialEffect()//explostion, burn, etc. must be defined in the actual spells
>>>>>>> master
    {

    }

<<<<<<< HEAD
    void Collide(Minion minion)
=======
    protected void Collide(Minion minion)
>>>>>>> master
    {
        source.notifyHit(minion);
        SpecialEffect();
    }
}
