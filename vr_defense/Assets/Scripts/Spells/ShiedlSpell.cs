using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//The ShieldSpell inplements Spell.
//It has its own healthpoints that are decreased everytime it hits a minion's rock
//and is deactivated once those HPs are at 0
public class ShiedlSpell : Spell
{

    public bool activated = false;
    public const float strength = 5.0f;
    private float life;
    public GameObject shield;

    public bool isDown = false;

    public void ActivateShield()
    {
        activated = true;
        life = strength * power;
        shield.SetActive(true);
        isDown = false;
    }

    public void DesactivateShield()
    {
        activated = false;
        shield.SetActive(false);
    }

    public override string SpellType()
    {
        return "SHIELD";
    }


    // Use this for initialization
    void Start()
    {
        restTime = 2f;
        power_inc = 1.4f;
        shield.SetActive(false);
    }

    public void hit(float damage)
    {
        life -= damage;
        Debug.Log("Life; " + life);
        if (life <= 0)
        {
            Debug.Log("shield down");
            isDown = true;
            DesactivateShield();
        }
    }
}
