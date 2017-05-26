using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMinion : Minion {

    //all these are there for instantiation when the minion pops an item at its death
    public Potion potion;
    public Hat hat;
    public Sword sword;

    public const float timeToLive = 12f;

    void PopItem()
    {
        float p = Random.value;

        //the popped item has a 90% chance to be a potion and a 10% chance to be a hat
        if (p < 0.9)
        {
            Item clone = Instantiate(potion);
            clone.transform.position = transform.position; 
        }
        else if (p < 1.0)
        {
            Item clone = Instantiate(hat);
            clone.transform.position = transform.position;

        }
        else {
            GameObject.Find("Sword").GetComponent<Sword>().generateSword();
            Debug.Log("New Sword!");
        }
    }

    override protected void AdditionalEffects()
    {
        PopItem();
    }

    //This way the minion disappears after a while if it hasn't been destroyed before
    void Awake()
    {
        Destroy(gameObject, timeToLive);
    }

    void OnDestroy()
    {
        if (!Dead())//if it's not dead it means it has been destroyed after 12 seconds.
            //in this case, we simply notify the source so it counts as dead at the source's level,
            //but no additional effecti is applied (i.e. no bonus)
        {
            source.NotifyMinionDeath();
        }
    }
}
