using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : Item {

    public Rigidbody body;
    const float timeToTarget = 7f;

	// Use this for initialization
	void Start () {
        /*float distance = transform.position.magnitude;
        Vector3 groundVector = transform.position;
        groundVector.y = 0f;
        float theta = Vector3.Angle(groundVector, new Vector3(0f, 0f, 1f));
        float playerHeight = 2f;
        body.velocity = -groundVector/timeToTarget;
        body.velocity.Set(body.velocity.x,10*playerHeight / timeToTarget + 0.5f * 9.81f * timeToTarget,body.velocity.z);*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
