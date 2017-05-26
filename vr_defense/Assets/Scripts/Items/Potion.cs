using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item {

    float healthPoints = 10f;
    public Rigidbody body;
    float speed = 1.5f;

    public float HealthPoints() { return healthPoints; }

    public void Destroy()
    {
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 10);
        Vector3 target = GameObject.Find("headCamera").transform.position;
        body.velocity = (target-transform.position).normalized* speed;
	}

    
    virtual protected void OnTriggerEnter(Collider col)
    {
        //if the potion hits the player, it heals him or her
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "headCamera")
        {
            GameObject.Find("Player").GetComponent<Player>().Heal(HealthPoints());

        }
    }

}
