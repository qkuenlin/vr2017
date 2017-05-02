using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public FireSpell fire;
    public ThunderSpell thunder;
    public ShiedlSpell shield;

    float healthPoints;
    uint experiencePoints;

    void Damage(float damage)
    {
        damage -= shield.ShieldFactor();
        healthPoints -= damage;
        if (healthPoints < 0.0f)
        {
            Die();
        }

    }

    void Die()
    {
        Debug.Log("You're dead, mate");
    }

	// Use this for initialization
	void Start () {
        healthPoints = 100;
        experiencePoints = 0;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
