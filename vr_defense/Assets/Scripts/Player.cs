using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public FireSpell fire;
    public ThunderSpell thunder;
    public ShiedlSpell shield;

    float healthPoints;
    float experiencePoints;
    float score;

    Vector3 position;

    public float HP()
    {
        return healthPoints;
    }

    public float XP()
    {
        return experiencePoints;
    }

    public void GiveXP(float XP)
    {
        experiencePoints += XP;
        score += Mathf.Max(0,XP);
        GameObject.Find("Canvas").GetComponent<GUIManager>().UpdateScore(healthPoints,experiencePoints);
    }

    public void Hit(float damage)
    {
        healthPoints -= Mathf.Max(damage,0);
        if (healthPoints < 0.0f)
        {
            Die();
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<GUIManager>().UpdateScore(healthPoints, experiencePoints);
        }

    }

    public float getScore()
    {
        return score;
    }

    public void Heal(float hp)
    {

        healthPoints += hp;
        GameObject.Find("Canvas").GetComponent<GUIManager>().UpdateScore(healthPoints, experiencePoints);

    }

    void Die()
    {
        Debug.Log("You're dead, mate");
        GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();

    }

    Vector3 HeadPosition()
    {
        return position+new Vector3(0,1,0);
    }

  

    // Use this for initialization
    void Start () {
        healthPoints = 100;
        experiencePoints = 0;
        position.Set(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Move(x, z);
        UpdateTransform();
	}

    void Move(float x, float z)
    {
        position.Set(0.3f*x,0.0f,0.3f*z);

    }

    void UpdateTransform()
    {
        transform.position = HeadPosition();
    }


}
