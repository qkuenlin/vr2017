using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShootMinion : Minion {

    public MinionProjectile projectile;

    override protected float AttackDistance() { return 7f; }

    override protected void Attack()
    {
        MinionProjectile clone = Instantiate(projectile);
        clone.body.transform.position = transform.position;
        clone.body.velocity = (GameObject.Find("headCamera").transform.position - transform.position).normalized*projectileSpeed;
        clone.body.angularVelocity = new Vector3(Random.value*10f,Random.value*10,Random.value*10f);
        clone.SetSource(this);

    }
}
