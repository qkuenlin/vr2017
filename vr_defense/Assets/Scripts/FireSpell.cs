using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireSpell : MonoBehaviour {
    public uint level;
    public Rigidbody fireball;

    void Throw(Vector3 origin, Vector3 direction, float speed)
    {
        Debug.Log("THROW !!");

        Rigidbody clone = Instantiate(fireball);
        clone.transform.position = origin;
        clone.AddForce(direction.normalized * speed);

    }
	// Use this for initialization
	void Start () {
        Debug.Log("START");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mouse = Input.mousePosition;
            Vector3 shootDirection = new Vector3(0f, (mouse.y - Screen.height / 2f) / Screen.height, 1f);

            Debug.Log(Input.mousePosition);
            Debug.Log(shootDirection);
            Throw(transform.position, shootDirection.normalized,10.0f/Time.deltaTime);
        }
	}
}
