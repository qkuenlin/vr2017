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
        Vector3 target=new Vector3(0,1.8f,0);
        body.velocity = (target-transform.position).normalized* speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
