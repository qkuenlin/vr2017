using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    uint waveCount=0;

    public MinionSpawnPoint ccSpawn;
    public MinionSpawnPoint randShootSpawn;
    public MinionSpawnPoint headShootSpawn;

    //bool paused = true;

    bool AllDone()
    {
        return ccSpawn.Done()
            && randShootSpawn.Done()
            && headShootSpawn.Done();
    }

    /**This defines what each waves consist of
     for now it's a bit random. Must be refined later */
    public void LaunchWave()
    {
        waveCount++;
        switch (waveCount)
        {
            case 0: break;
            case 1: ccSpawn.Spawn(5,1f);break;
            case 2: randShootSpawn.Spawn(5, 1f);break;
            case 3: headShootSpawn.Spawn(5, 1f); break;
            case 4:
                {
                    ccSpawn.Spawn(5, 1f);
                    randShootSpawn.Spawn(5, 1f);
                    break;
                }

            case 5:
                {
                    headShootSpawn.Spawn(5, 1f);
                    randShootSpawn.Spawn(5, 1f);
                    break;
                }
            default:
                {
                    Stop();
                    break;
                }
        }
    }

   /* public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }*/

    public bool Done()
    {
        return AllDone();
    }

    public void Stop()
    {
        Destroy(this);
    }

    // Use this for initialization
    void Start () {
        Debug.Log("WaveManger started");
	}

	// Update is called once per frame
	void Update () {
        /*if (AllDone())
        {
            Debug.Log("wave " + waveCount + " is over");
            waveCount++;
            //Pause();
        }*/

        /*if (AllDone() && !paused)
        {
            Debug.Log("Wave " + waveCount + " is over, launching new wave");
            waveCount++;
            LaunchWave();
        }*/
	}
}
