using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{

    public GameManager gameManager;
   // public WaveManager waveManager;
    //public MenuManager menuManager;

    public ThunderSpell thunder;
    public FireSpell fire;
    public ShiedlSpell shield;

    public Text scoreText;
    public Text Main_Headline;
    public Text Sub_text;

    public Text ThunderCost;
    public Text FireCost;
    public Text ShieldCost;

    public float maxTimer = 2f;

    private GameManager.Mode previousState;

    private float timer;
    public float sub_text_timer = 0;


    // Use this for initialization
    void Start()
    {
        previousState = GameManager.Mode.MENU;

    }

    // Update is called once per frame
    void Update()
    {
        string set_text = "";
        float color = 0;

        ThunderCost.text = "";
        FireCost.text = "";
        ShieldCost.text = "";

        switch (gameManager.mode)
        {
            case GameManager.Mode.IDLE:
                {
                    break;
                }
            case GameManager.Mode.MENU:
                {
                    if (previousState == GameManager.Mode.WAVE)
                    {
                        timer = maxTimer;
                        previousState = GameManager.Mode.MENU;
                    }
                    if (timer > 0)
                    {
                        set_text = "TIME TO UPGRADE YOUR SPELLS";
                        timer -= Time.deltaTime;
                        color = timer / maxTimer;
                    }
                    else
                    {
                        float count = gameManager.getCountdown();
                        set_text = ""+ Mathf.CeilToInt(count);
                        color = 1 - (Mathf.Ceil(count) - count);
                    }

                    ThunderCost.text = thunder.getCost().ToString() + " XP";
                    FireCost.text = fire.getCost().ToString() + " XP";
                    ShieldCost.text = shield.getCost().ToString() + " XP";

                    break;
                }
            case GameManager.Mode.WAVE:
                {
                    if (previousState == GameManager.Mode.MENU)
                    {
                        timer = maxTimer;
                        previousState = GameManager.Mode.WAVE;
                    }
                    if (timer > 0)
                    {
                        set_text = "WAVE " + gameManager.WaveCount() + " STARTING";
                        timer -= Time.deltaTime;
                        color = timer / maxTimer;
                    }
                    break;
                }
            case GameManager.Mode.DEAD:
                {
                    scoreText.text = "";
                    set_text = "YOU DIED\n\n\n SCORE: " + GameObject.Find("Player").GetComponent<Player>().getScore();
                    color = 1.0f;
                    break;
                }
        }

        Main_Headline.text = set_text;
        color *= color;
        Main_Headline.color = new Color(1f, 1f, 1f, color);

        float color_subText = sub_text_timer / 2f;
        Sub_text.color = new Color(1f, 1f, 1f, color_subText);

        sub_text_timer -= Time.deltaTime;

        //update player data
    }

    public void UpdateScore(float hp, float xp)
    {
       // Debug.Log("UPDATED SCORE");
        string score = "";
        score += "HP : " + hp + "\n";
        score += "XP : " + xp + "\n";
        scoreText.text = score;
    }

    public void setSub(string text)
    {
        sub_text_timer = 2f;
        Sub_text.text = text;
    }
}
