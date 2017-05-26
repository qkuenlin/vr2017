using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The fireball class simply implements the SpellProjectil abstract class
//and adds the notion of time-to-live to projectiles.
//This is done to avoid having hundreds of fireballs on the long run
public class fireBall : SpellProjectile {

    public float lifetime = 4.0f;


    void Awake()
    {
        Destroy(gameObject, lifetime);
    }
}
