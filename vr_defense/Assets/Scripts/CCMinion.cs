using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMinion : Minion {

    override protected float AttackDistance() { return 0.5f; }

    override protected void Attack() {
        ShiedlSpell shield = GameObject.Find("ShieldSpell").GetComponent<ShiedlSpell>();
        if (shield.activated)
        {
            shield.hit(AttackDamage());
        }
        else
        {
            GameObject.Find("Player").GetComponent<Player>().Hit(AttackDamage());
        }
    }

}
