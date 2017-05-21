using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour {

    public Spell spell;
    public GameObject ob;


    public void OnTriggerEnter(Collider col)
    {
        Debug.Log("token " + col.gameObject.name);
        if (col.gameObject.name == "R" || col.gameObject.name == "L")
        {
            if (GameObject.Find("Player").GetComponent<Player>().XP() > spell.getCost())
            {
                GameObject.Find("Player").GetComponent<Player>().GiveXP(-spell.getCost());
                spell.upgrade();
            }
        }
    }

    public void setActive(bool b)
    {
        ob.SetActive(b);
    }

}
