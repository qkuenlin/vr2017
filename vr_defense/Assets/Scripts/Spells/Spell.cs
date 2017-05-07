using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

    public Player wizard;
    protected uint level = 1;
    //public Rigidbody fireball;
    protected float power = 0.0f;

    public void notifyHit(Minion minion)
    {
        wizard.GiveXP(minion.Hit(Damage(), SpellType()));
    }

    virtual public float Damage()
    {
        return level * power;
    }

    virtual public string SpellType()
    {
        return "none";
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
