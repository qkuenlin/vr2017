using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpell : Spell {


    public ThunderBall thunderBall;

    public override string SpellType()
    {
        return "thunder";
    }

    // Update is called once per frame
    void Update () {
    }
}
