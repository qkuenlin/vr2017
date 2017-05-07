using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : Spell {
   
    public fireBall fireball;

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
	}

    public override string SpellType() 
    {
        return "fire";
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mouse = Input.mousePosition;
            Vector3 shootDirection = new Vector3(0f, (mouse.y - Screen.height / 2f) / Screen.height, 1f);

           // Debug.Log(Input.mousePosition);
            //Debug.Log(shootDirection);
            Throw(transform.position, shootDirection.normalized,10.0f/Time.deltaTime);
        }
	}
}
