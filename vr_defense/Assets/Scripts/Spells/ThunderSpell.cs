﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpell : Spell {


    public ThunderBall thunderBall;

    public override string SpellType()
    {
        return "thunder";
    }

    public ThunderBall newThunderBall()
    {
        ThunderBall clone = Instantiate(thunderBall);
        clone.SetSource(this);
        return clone;
    }

    void Start()
    {
        
        power = 1f;
        // Instantiate for mouse control //
        Instantiate(thunderBall);
        thunderBall.SetSource(this);
        
    }

    // Update is called once per frame
    void Update () {
    }
}