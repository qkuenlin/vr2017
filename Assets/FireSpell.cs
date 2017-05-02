using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : MonoBehaviour {
    public uint level;
    public Rigidbody fireball;

    void Throw(Vector3 origin, Vector3 direction, float speed)
    {
        Debug.Log("THROW !!");
        Rigidbody clone;
        clone = Instantiate(fireball);
        clone.position = origin;
        clone.velocity = direction.normalized * speed;
    }
	// Use this for initialization
	void Start () {
        //fireball = new SphereCollider();
        Debug.Log("START");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            Throw(new Vector3(0.0f, 1.0f, 0.0f),new Vector3(0.0f,0.0f,1.0f),1.0f);
        }
	}
}
