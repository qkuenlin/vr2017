using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public WaveManager waveManager;
    public MenuManager menuManager;

    public enum Mode {IDLE,WAVE,MENU,DEAD};

    public Mode mode = Mode.IDLE;

    public uint WaveCount()
    {
        return waveManager.waveCount;
    }

    public float getCountdown()
    {
        return menuManager.getCountdown();
    }

    public void GameOver()
    {
        menuManager.Pause();
        waveManager.Pause();
        mode = Mode.DEAD;
        Debug.Log("gameover");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.UnloadScene(SceneManager.GetActiveScene());
            SceneManager.LoadScene("game");
        }


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
            default: break;
        }

	}
}
