using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawnPoint : MonoBehaviour {

    uint level = 1;
    public Minion minion;
    public BonusMinion bonusMinion;

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
    void Spawn()
    {
        float p = Random.value;
        Minion clone;
        if (p < 0.9)//10% chance that the new minion is a bonus one
        {
            clone = Instantiate(minion);
        }else
        {
            clone = Instantiate(bonusMinion); 
        }
        clone.body.transform.position = transform.position;
        clone.body.velocity = new Vector3(0f, 0f, -1.5f);
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

    // Update is called once per frame
    void Update()
    {
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
        
    }
}
