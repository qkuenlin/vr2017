using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawnPoint : MonoBehaviour {

    public uint level;
    public Minion minion;

    float spawnTime = 0f;
    public const float spawnInterval = 0.5f;

    void Spawn()
    {
        Minion clone = Instantiate(minion);
        clone.body.transform.position = transform.position;
        clone.body.velocity = new Vector3(0f, 0f, -1.5f);
    }
    // Use this for initialization
    void Start()
    {
        //Debug.Log("START");
        Spawn();
    }




    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTime > spawnInterval)
        {
            spawnTime = Time.time;
            Spawn();
        }
    }
}
