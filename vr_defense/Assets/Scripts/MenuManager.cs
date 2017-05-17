using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public Object fireToken;
    public Object thunderToken;
    public Object shieldToken;

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
        Instantiate(fireToken);
        Instantiate(thunderToken);
        Instantiate(shieldToken);
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
    }
	
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            countdown -= Time.deltaTime;
        }

        if ( countdown <= 0)
        {
            done = true;
        }
	}
}
