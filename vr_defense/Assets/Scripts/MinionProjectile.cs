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
        Player player = (Player)col.GetComponent("Player");
        if (player != null)
        {
            Debug.Log("player hit by projectile !");
            player.Hit(Damage());
        }
    }

    void Awake()
    {
        Destroy(gameObject, 20);
    }
}
