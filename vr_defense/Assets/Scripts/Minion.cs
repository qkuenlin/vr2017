﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour {

    public Rigidbody body;
    float HealthPoints;
    uint level;
    string resistance = "none";
<<<<<<< HEAD
=======
    MinionSpawnPoint source;

    bool dead = false;//this is needed because it isn't destroyed right away. 
                      //So it can be hit again after dying, thus calling Die() again


    public void SetSource(MinionSpawnPoint spawnPoint)
    {
        source = spawnPoint;
    }
>>>>>>> master

    public float ExperiencePoints()
    {
        return level;
    }

    public float Hit(float damage, string spellType)
    {
<<<<<<< HEAD
        Debug.Log("MINION HIT ! received "+damage+" damage of type "+spellType);
        if (spellType != resistance)
        {
            HealthPoints -= damage;
            if (HealthPoints <= 0f)
=======
        Debug.Log("MINION HIT ! "+gameObject.name+" received "+damage+" damage of type "+spellType);
        if (spellType != resistance)
        {
            HealthPoints -= damage;
            if (HealthPoints <= 0f && !dead)
>>>>>>> master
            {
                Die();
                return ExperiencePoints();
   
            }
        }
        return 0f;
    }

    void PopItem()//TODO
    {

    }

    void Die()
    {
<<<<<<< HEAD
        Debug.Log("a minion has died");
        PopItem();
=======
        dead = true;
        //Debug.Log("a minion has died");
        PopItem();
        source.NotifyMinionDeath();
>>>>>>> master
        Destroy(gameObject, 0.5f);//die after half a second
    }



	// Use this for initialization
	void Start()
    {
        HealthPoints = 1;
        level = 1;
        Debug.Log("created a minion");
        //body.velocity = new Vector3(0f, 0f, -1f);
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position += new Vector3(0f, 0f, -1f*Time.deltaTime );
       // Debug.Log(body.transform.position);
	}
}