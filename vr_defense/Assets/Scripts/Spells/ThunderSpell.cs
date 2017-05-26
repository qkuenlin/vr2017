using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpell : Spell {


    public ThunderBall thunderBall;

    public override string SpellType()
    {
        return "THUNDER";
    }

    /*public ThunderBall newThunderBall()
    {
        ThunderBall clone = Instantiate(thunderBall);
        clone.SetSource(this);
        return clone;
    }*/

    void Start()
    {
        restTime = 3f;
        power = 1f;
        power_inc = 1.3f;
        // Instantiate for mouse control //
        thunderBall = Instantiate(thunderBall);
        thunderBall.SetSource(this);
        Debug.Log("thunderball instantiated");
    }

    // Update is called once per frame
    void Update () {
    }
}
