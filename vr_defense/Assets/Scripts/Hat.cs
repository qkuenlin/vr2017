using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : Item {

    public Rigidbody body;
    const float timeToTarget = 7f;
    float speed = 2.5f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 10);
        Vector3 target = GameObject.Find("headCamera").transform.position;
        body.velocity = (target - transform.position).normalized * speed;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "headCamera") {
            int p = Random.Range(0, 3);
            if (p == 0)
            {
                GameObject.Find("FireballSpell").GetComponent<FireSpell> ().upgrade();
            }else if (p == 1)
            {
                GameObject.Find("ThunderSpell").GetComponent<ThunderSpell>().upgrade();
            }
            else
            {
                GameObject.Find("ShieldSpell").GetComponent<ShiedlSpell>().upgrade();
            }

        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
