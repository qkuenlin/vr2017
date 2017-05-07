using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpell : Spell {


    public ThunderBall thunderBall;

    public override string SpellType()
    {
        return "thunder";
    }

    void Start()
    {
        power = 1f;
        Instantiate(thunderBall);
        thunderBall.SetSource(this);
    }

    // Update is called once per frame
    void Update () {
    }
}
