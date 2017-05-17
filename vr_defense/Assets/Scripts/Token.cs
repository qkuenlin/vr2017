using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour {

    public Spell spell;
    public GameObject ob;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void OnTriggerEnter(Collider col)
    {
        spell.upgrade();
    }

    public void setActive(bool b)
    {
        ob.SetActive(b);
    }

}
