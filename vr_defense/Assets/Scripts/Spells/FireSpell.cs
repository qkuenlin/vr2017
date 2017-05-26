using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The FireSpell class implements the abstract Spell class.
//It contains everything we need to throw fireballs and stores the related data
public class FireSpell : Spell {
   
    //A fireBall to be cloned at every throw
    public fireBall fireball;

    //Throws a new fireball from a point in the desired direction at a specified speed
    public void Throw(Vector3 origin, Vector3 direction, float speed)
    {
        fireBall clone = Instantiate(fireball);
        clone.body.transform.position = origin;
        clone.body.AddForce(direction.normalized * speed);
        clone.SetSource(this);

    }

	// Use this for initialization
	void Start () {
        power = 1f;
        power_inc = 1.5f;
        restTime = 0.8f;
	}

    public override string SpellType() 
    {
        return "FIRE";
    }


    // Update is called once per frame
    void Update () {
        //This was used when we couldn't use kinect
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mouse = Input.mousePosition;
            Vector3 shootDirection = new Vector3((2f * mouse.x / Screen.width - 1f), (mouse.y - Screen.height / 2f) / Screen.height, 1f).normalized;
            Throw(transform.position, shootDirection.normalized,1.0f/Time.deltaTime);
        }
	}
}
