using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour {

    public Rigidbody body;
    float HealthPoints;
    uint level;
    string resistance = "none";

    public float ExperiencePoints()
    {
        return level;
    }

    public float Hit(float damage, Spell spell)
    {
        if (spell.SpellType() != resistance)
        {
            HealthPoints -= damage;
            if (HealthPoints < 0f)
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
        Debug.Log("a minion has died");
        PopItem();
        Destroy(gameObject, 1.0f);//die after one second
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(gameObject.name +" collided with " + col.gameObject.name);
        //player.Hit(MinionDamage());
    }

	// Use this for initialization
	void Start()
    {
        HealthPoints = 1;
        level = 0;

        body.velocity.Set(0f,0f,-1f);
        Debug.Log("created a minion");
    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(body.transform.position);
	}
}
