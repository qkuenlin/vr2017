using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpell : Spell {

    float chargeStart;
    bool charging = false;

    const float maxCharge = 5.0f;

    public SphereCollider collider;


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
            Debug.Log("Starting charge");
            charging = true;
            chargeStart = Time.time;//time since start of game
        }
        if (Time.time - chargeStart > maxCharge)
        {
            Release();
        }
        else
        {
            collider.transform.position = target;
            collider.transform.localScale = new Vector3(1f, 1f, 1f);
            collider.transform.localScale *= targetRadius();
            collider.radius = targetRadius();
        }
    }

    void Release()
    {
        Debug.Log("realeased");
        charging = false;
        collider.transform.localScale = new Vector3(0f, 0f, 0f);
        collider.radius = 0f;
        //effects and attacks
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            Vector3 target = Input.mousePosition;
            target.Set(10f*(target.x/Screen.width-0.5f),0f,7f+10f*(target.y/Screen.height-0.5f));
            Charge(target);
        }else if (Input.GetMouseButtonUp(1))
        {
            Release();
        }
    }
}
