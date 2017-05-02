using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpell : MonoBehaviour {

    float chargeStart;
    bool charging = false;

    const float maxCharge = 5.0f;

    float targetRadius()
    {
        float chargeTime = Time.time - chargeStart;
        if (charging && chargeTime < maxCharge)
        {
            return Mathf.Max(0.0f, chargeTime / 5.0f);
        }
        return 0.0f;
    }

    void Charge(Vector3 target)
    {
        if (!charging)
        {
            charging = true;
            chargeStart = Time.time;//time since start of game
        }
        //DrawTarget();
    }

    void Release()
    {
        charging = false;
        //effects and attacks
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
