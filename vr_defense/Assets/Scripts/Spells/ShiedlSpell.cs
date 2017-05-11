using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiedlSpell : Spell {

    public bool activated = false;
    public const float strength = 10.0f;
    public GameObject shield;

    public float ShieldFactor()
    {
        if (activated)
        {
            return level * strength;
        }
        return 0.0f;
    }

    public void ActivateShield()
    {
        activated = true;
        shield.SetActive(true);
    }

    public void DesactivateSheidl()
    {
        activated = false;
        shield.SetActive(false);
    }


    // Use this for initialization
    void Start () {
        shield.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
