using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    bool paused = true;
    float time = 0f;
    bool done = true;
    float menuTime = 2f;

    public void Pause()
    {
        Debug.Log("MenuManager paused");
        paused = true;
    }

    public void Resume()
    {
        Debug.Log("MenuManager resumed");
        paused = false;
        done = false;
        time = Time.time;
    }

    public void ActivateMenu()
    {

    }

    public bool Done()
    {
        return done;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!paused && Time.time - time > menuTime)
        {
            done = true;
        }
	}
}
