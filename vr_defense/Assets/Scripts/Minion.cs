using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minion : MonoBehaviour {

    public Rigidbody body;

    float speed = 1f;
    float HealthPoints;
    uint level;
    string resistance = "none";
    MinionSpawnPoint source;

    bool dead = false;//this is needed because it isn't destroyed right away. 
                      //So it can be hit again after dying, thus calling Die() again


    public void SetSpeed(float s)
    {
        speed = s;
    }

    public void SetSource(MinionSpawnPoint spawnPoint)
    {
        source = spawnPoint;
    }

    public float ExperiencePoints()
    {
        return level;
    }

    public float Hit(float damage, string spellType)
    {
        //Debug.Log("MINION HIT ! "+gameObject.name+" received "+damage+" damage of type "+spellType);
        if (spellType != resistance)
        {
            HealthPoints -= damage;
            if (HealthPoints <= 0f && !dead)
            {
                Die();
                return ExperiencePoints();
   
            }
        }
        return 0f;
    }

    

    void Die()
    {
        dead = true;
        //Debug.Log("a minion has died");
        AdditionalEffects();
        source.NotifyMinionDeath();
        Destroy(gameObject, 0.5f);//die after half a second
    }

    virtual protected void AdditionalEffects() { }

    //When a minion reaches its distance attack, it stops and starts attacking
    virtual protected float AttackDistance() { return 0f; }

	// Use this for initialization
	void Start()
    {
        HealthPoints = 1;
        level = 1;
        //Debug.Log("created a minion");
        body.velocity = new Vector3(0, 0, -1f);
    }
	
	// Update is called once per frame
	void Update () {
        float p = Random.value;
        //randomizes a bit the minions' movements
        Vector3 direction = body.velocity.normalized;
        //the direction has a 5% chance of changing
        if (p > 0.85f)
        {
            p = Random.value;
            p = (p - 0.5f) * (Mathf.PI / 2f);
            direction = new Vector3(Mathf.Sin(p), 0f, Mathf.Cos(p)) * speed* (-1f);
        }
        Vector3 target = new Vector3(0f, 0f, 0f);//this will be replaced by the player's position

        //the final velocity is a mean between a random direction and the target's direction
        //this rouhgly ensures that the minion goes towards the player
        body.velocity = (0.7f*direction + 0.3f*(target-transform.position).normalized).normalized*speed;
	}
}
