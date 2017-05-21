using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionProjectile : MonoBehaviour
{
    public Minion source;
    public Rigidbody body;

    public void SetSource(Minion minion)
    {
        source = minion;
    }

    public float Damage()
    {
        return source.AttackDamage();
    }


    virtual protected void OnTriggerEnter(Collider col)
    {
       // Debug.Log("projectile " + col.gameObject.name);
        if (col.gameObject.name == "headCamera")
        {
             GameObject.Find("Player").GetComponent<Player>().Hit(Damage()) ;

        }else if(col.gameObject.name == "Shield")
        {
            GameObject.Find("ShieldSpell").GetComponent<ShiedlSpell>().hit(Damage());
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        Destroy(gameObject, 20);
    }
}
