using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public FireSpell fire;
    public ThunderSpell thunder;
    public ShiedlSpell shield;

    float healthPoints;
    float experiencePoints;
    Vector3 position;

    public void GiveXP(float XP)
    {
        experiencePoints += XP;
    }

    void Hit(float damage)
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

    Vector3 HeadPosition()
    {
        return position+new Vector3(0,2,0);
    }

	// Use this for initialization
	void Start () {
        healthPoints = 100;
        experiencePoints = 0;
        position.Set(0, 0, 0);

        fire = GetComponent<FireSpell>();
        thunder = GetComponent<ThunderSpell>();
        shield = GetComponent<ShiedlSpell>();
	}
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Move(x, z);
        UpdateTransform();
	}

    void Move(float x, float z)
    {
        position.Set(0.3f*x,0.0f,0.3f*z);

    }

    void UpdateTransform()
    {
        transform.position = HeadPosition();
    }


}
