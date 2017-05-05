using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public WaveManager waveManager;
    public MenuManager menuManager;

    enum Mode {IDLE,WAVE,MENU};

    Mode mode = Mode.IDLE;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        switch (mode)
        {
            case Mode.IDLE:
                {
                    Debug.Log("starting game ! ");
                    mode = Mode.WAVE;
                    waveManager.LaunchWave();
                   // waveManager.Resume();
                    break;
                }
            case Mode.MENU:
                {
                    if (menuManager.Done())
                    {
                        Debug.Log("menuManager is done. Switching to wave mode");
                        menuManager.Pause();
                        waveManager.LaunchWave();
                       // waveManager.Resume();
                        mode = Mode.WAVE;
                    }
                    break;
                }
            case Mode.WAVE:
                {
                    if (waveManager.Done())
                    {
                        Debug.Log("WaveManager is done. Switching to menu mode");
                        //waveManager.Pause();
                        menuManager.Resume();
                        menuManager.ActivateMenu();
                        mode = Mode.MENU;
                    }
                    break;
                }
        }
	}
}
