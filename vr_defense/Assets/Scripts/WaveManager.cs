using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    public uint waveCount=0;

    public MinionSpawnPoint ccSpawn;
    public MinionSpawnPoint randShootSpawn;
    public MinionSpawnPoint headShootSpawn;

    public const uint wavesPerLevel = 5;

    bool paused = true;

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

        uint waveLevel = waveCount / wavesPerLevel+1;
        uint minionNbr = 5 + waveLevel;
        float spawnInterval = 1f;

        switch (waveCount % wavesPerLevel + 1)
        {
            case 0: break;
            case 1: ccSpawn.Spawn(minionNbr, waveLevel, spawnInterval);break;
            case 2: headShootSpawn.Spawn(minionNbr, waveLevel, spawnInterval); break;
            case 3: headShootSpawn.Spawn(minionNbr, waveLevel, spawnInterval); break;
            case 4:
                {
                    ccSpawn.Spawn(minionNbr, waveLevel, spawnInterval);
                    randShootSpawn.Spawn(minionNbr, waveLevel, spawnInterval);
                    break;
                }

            case 5:
                {
                    headShootSpawn.Spawn(minionNbr, waveLevel, spawnInterval);
                    randShootSpawn.Spawn(minionNbr, waveLevel, spawnInterval);
                    break;
                }
            default:
                {
                    Stop();
                    break;
                }
        }

        waveCount++;

    }

    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }

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
       /* if (AllDone())
        {
            Debug.Log("wave " + waveCount + " is over");
            waveCount++;
            //Pause();
        }*/
        /*
        if (AllDone() && !paused)
        {
            Debug.Log("Wave " + waveCount + " is over, launching new wave");
            waveCount++;
            LaunchWave();
        }*/
	}
}
