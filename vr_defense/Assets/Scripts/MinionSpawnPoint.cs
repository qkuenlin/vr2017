﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawnPoint : MonoBehaviour {

<<<<<<< HEAD
    public uint level;
    public Minion minion;

    float spawnTime = 0f;
    public const float spawnInterval = 0.5f;

=======
    uint level = 1;
    public Minion minion;

    float spawnTime = 0f;
    float spawnInterval = 0.5f;

    bool doneSpawning = true;
    uint deadMinionsCount = 0; //how many minions have died
    uint spawnedMinionsCount = 0; //how many minions have been spawned
    uint spawnedMinionsTarget = 0; //how many minions must be spawned

    /**To be called by the spawned minions when dying*/
    public void NotifyMinionDeath()
    {
        deadMinionsCount++;
       // Debug.Log(gameObject.name + " has been notified of a death, count is now at " + deadMinionsCount);
    }

    /**Sets the spawnPoint to spawn minionNbr minions, with spawnInterval seconds between each minion*/
    public void Spawn(uint minionNbr, float interval)
    {
        Debug.Log(gameObject.name + " received new wave order. Spawning one minion every " + interval + "sec. " + minionNbr + " times");
        if (doneSpawning)
        {
            doneSpawning = false;
            spawnInterval = interval;
            spawnedMinionsTarget = minionNbr;
        }
    }

    /**Spawns one minion. Velocity must still be defined depending on level or something*/
>>>>>>> master
    void Spawn()
    {
        Minion clone = Instantiate(minion);
        clone.body.transform.position = transform.position;
        clone.body.velocity = new Vector3(0f, 0f, -1.5f);
<<<<<<< HEAD
    }
    // Use this for initialization
    void Start()
    {
        //Debug.Log("START");
        Spawn();
    }



=======
        clone.SetSource(this);

    }

    /**The spawning point is done if it's spawned all the required minions and if they are all dead*/
    public bool Done()
    {
        if(doneSpawning && deadMinionsCount >= spawnedMinionsTarget)
        {
            deadMinionsCount = 0;
            spawnedMinionsCount = 0;
            spawnedMinionsTarget = 0;
            return true;
        }
        return false;
    }

    // Use this for initialization
    void Start()
    {

    }
>>>>>>> master

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if (Time.time - spawnTime > spawnInterval)
        {
            spawnTime = Time.time;
            Spawn();
        }
=======
        //Debug.Log(doneSpawning + " " + (Time.time - spawnTime) + " "+spawnedMinionsCount);
        /*This spawns as many minions as requested by the WaveManager*/
        if (!doneSpawning && Time.time-spawnTime>spawnInterval && spawnedMinionsCount<spawnedMinionsTarget)
        {
            Debug.Log("spawning a new minion");
            Spawn();
            spawnedMinionsCount++;
            spawnTime = Time.time;
            if (spawnedMinionsCount == spawnedMinionsTarget)
            {
                doneSpawning = true;
                Debug.Log(gameObject.name + " is done spawning minions");
            }
        }
        
>>>>>>> master
    }
}