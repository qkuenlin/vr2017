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

    virtual protected void OnTriggerEnter(Collider col)
    {
        Debug.Log("smt touch me! " + col);
        float price = 5 * spell.getLevel();
        if (GameObject.Find("Player").GetComponent<Player>().XP() > price)
        {
            GameObject.Find("Player").GetComponent<Player>().GiveXP(-price);
            spell.upgrade();
        }
        
    }

    public void setActive(bool b)
    {
        ob.SetActive(b);
    }

}
