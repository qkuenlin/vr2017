using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireSpell : Spell {
    public uint level;
    //public Rigidbody fireball;
    const float power = 1.0f;

    public fireBall fireball;

    float SpellDamage()
    {
        return level * power;
    }

    void Throw(Vector3 origin, Vector3 direction, float speed)
    {
        //Debug.Log("THROW !!");
        /*Rigidbody clone = Instantiate(fireball);
        clone.transform.position = origin;
        clone.AddForce(direction.normalized * speed);
        */

        fireBall clone = Instantiate(fireball);
        clone.body.transform.position = origin;
        clone.body.AddForce(direction.normalized * speed);
        clone.SetSource(this);

    }
	// Use this for initialization
	void Start () {
        //Debug.Log("START");
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
