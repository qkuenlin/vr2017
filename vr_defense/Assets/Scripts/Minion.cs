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

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(gameObject.name + " collided with " + col.gameObject.name);
        if (col.gameObject.GetType() == typeof(Minion))
        {
            Debug.Log("MINION !");
            // Collide((Minion)col.gameObject);
        }
        //Collide(col.gameObject);
    }


	// Use this for initialization
	void Start()
    {
        HealthPoints = 1;
        level = 0;
        Debug.Log("created a minion");
        body.velocity = new Vector3(0f, 0f, -1f);
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position += new Vector3(0f, 0f, -1f*Time.deltaTime );
       // Debug.Log(body.transform.position);
	}
}
