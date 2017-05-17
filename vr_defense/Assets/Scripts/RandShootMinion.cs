using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandShootMinion : Minion {

    public const float variationAngle = 15f;

    public MinionProjectile projectile;

    override protected float AttackDistance() { return 5f; }

    override protected void Attack()
    {
        MinionProjectile clone = Instantiate(projectile);
        clone.body.transform.position = transform.position;
        Vector3 generalDirection = (GameObject.Find("headCamera").transform.position - transform.position).normalized;

        float verticalVariation = Random.value* 2*variationAngle - variationAngle;
        float horizontalVariation = Random.value * 2 * variationAngle - variationAngle;

        generalDirection = Quaternion.Euler( horizontalVariation, verticalVariation, 0) * generalDirection;

        clone.body.velocity = generalDirection.normalized * projectileSpeed;
        clone.SetSource(this);

    }

}
