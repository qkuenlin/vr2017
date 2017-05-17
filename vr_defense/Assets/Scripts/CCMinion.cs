using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMinion : Minion {

    override protected float AttackDistance() { return 1.0f; }

    override protected void Attack() {
        GameObject.Find("Player").GetComponent<Player>().Hit(AttackDamage()) ;

    }

}
