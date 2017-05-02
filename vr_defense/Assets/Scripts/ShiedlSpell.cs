using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiedlSpell : Spell {

    bool activated = false;
    public uint level;
    public const float strength = 10.0f;

    public float ShieldFactor()
    {
        if (activated)
        {
            return level * strength;
        }
        return 0.0f;
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
