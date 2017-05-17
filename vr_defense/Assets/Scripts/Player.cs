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

    public float HP()
    {
        return healthPoints;
    }

    public float XP()
    {
        return experiencePoints;
    }

    public void GiveXP(float XP)
    {
        experiencePoints += XP;
        GameObject.Find("Canvas").GetComponent<GUIManager>().UpdateScore(healthPoints,experiencePoints);
    }

    public void Hit(float damage)
    {
        damage -= shield.ShieldFactor();
        healthPoints -= damage;
        Debug.Log("you've been hit and lost " + damage + " health points");
        if (healthPoints < 0.0f)
        {
            Die();
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<GUIManager>().UpdateScore(healthPoints, experiencePoints);
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

    /**how to handle the various collisions with 
     - Minions
     - Items
     - Menus
     There might be a better way to do it but I don't know of it*/
    void OnTriggerEnter(Collider col)
    {
        Potion potion = (Potion)col.GetComponent("Potion");
        if(potion != null)
        {
            healthPoints += potion.HealthPoints();
            potion.Destroy();
        }
        else
        {

        }       
    }

    // Use this for initialization
    void Start () {
        healthPoints = 100;
        experiencePoints = 0;
        position.Set(0, 0, 0);

        /*fire = GetComponent<FireSpell>();
        thunder = GetComponent<ThunderSpell>();
        shield = GetComponent<ShiedlSpell>();*/
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
