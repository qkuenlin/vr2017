using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpell : Spell {


    public ThunderBall thunderBall;

	// Use this for initialization
	void Start () {
        power = 1f;
        Instantiate(thunderBall);
        thunderBall.SetSource(this);
	}

    public override string SpellType()
    {
        return "thunder";
    }

    // Update is called once per frame
    void Update () {
    }
}
