using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Object
{
    public Transform transform;
    public SphereCollider collider;
}

public class FireSpell : MonoBehaviour {
    public uint level;
    public Fireball fireball;

    void Throw(Vector3 origin, Vector3 direction, float speed)
    {
        Debug.Log("THROW !!");
        Fireball clone;
        clone = Instantiate(fireball);
        clone.transform.
        clone.velocity = direction.normalized * speed;
    }
	// Use this for initialization
	void Start () {
        fireball = GetComponent<Rigidbody>();
        Debug.Log("START");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            Throw(Input.mousePosition, new Vector3(0.0f,0.0f,1.0f),1.0f);
        }
	}
}
