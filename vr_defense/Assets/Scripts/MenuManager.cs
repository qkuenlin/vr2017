using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public Token fireToken;
    public Token thunderToken;
    public Token shieldToken;

    private bool paused = true;
    private float countdown = 0f;
    private bool done = true;
    private float menuTime = 10f;

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
    }

    public void ActivateMenu()
    {
        countdown = menuTime;
        done = false;
        fireToken.setActive(true);
        thunderToken.setActive(true);
        shieldToken.setActive(true);
    }

    public bool Done()
    {
        return done;
    }

    public float getCountdown()
    {
        return countdown;
    }

	// Use this for initialization
	void Start () {
        
        fireToken.setActive(false);
        thunderToken.setActive(false);
        shieldToken.setActive(false);
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            countdown -= Time.deltaTime;
        }

        if ( countdown <= 0)
        {
            
            fireToken.setActive(false);
            thunderToken.setActive(false);
            shieldToken.setActive(false);
            
            done = true;
        }
	}
}
