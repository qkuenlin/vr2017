using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMinion : Minion {

    public Potion potion;
    public Hat hat;
    public Sword sword;

    void PopItem()//TODO
    {
      //  Debug.Log("Popping new item");
        float p = Random.value;

        if (p < 0.9)
        {
            Item clone = Instantiate(potion);
            clone.transform.position = transform.position;
        }else if (p < 0.95)
        {
           // item = new Sword();
        }else
        {
            //item = new Hat();
        }
    }

    override protected void AdditionalEffects()
    {
        PopItem();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
