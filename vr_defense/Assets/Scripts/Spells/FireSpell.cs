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
        restTime = 0.8f;
	}

    public override string SpellType() 
    {
        return "Fire";
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mouse = Input.mousePosition;
            Vector3 shootDirection = new Vector3((2f * mouse.x / Screen.width - 1f), (mouse.y - Screen.height / 2f) / Screen.height, 1f).normalized;

           // Debug.Log(Input.mousePosition);
            //Debug.Log(shootDirection);
            Throw(transform.position, shootDirection.normalized,1.0f/Time.deltaTime);
        }
	}
}
