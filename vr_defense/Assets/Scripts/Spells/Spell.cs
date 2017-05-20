﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

    public Player wizard;

    protected uint level = 1;
    protected float power = 0.0f;
    protected float restTime;

    public void notifyHit(Minion minion)
    {
        wizard.GiveXP(minion.Hit(Damage(), SpellType()));
    }

    virtual public uint getLevel()
    {
        return level;
    }

    virtual public float getRestTime()
    {
        return restTime;
    }

    virtual public float getPower()
    {
        return power;
    }

    virtual public void upgrade()
    {
        level += 1;
        restTime *= 0.95f;
        power *= 1.1f;
        Debug.Log(SpellType() + " has been upgraded! level=" + level + "  rest time=" + restTime + "  power=" + power);
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
