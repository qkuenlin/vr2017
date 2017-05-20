using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMinion : Minion {

    public Potion potion;
    public Hat hat;
    public Sword sword;

    public const float timeToLive = 12f;

    float spawnTime = 0f;
    void PopItem()//TODO
    {
      //  Debug.Log("Popping new item");
        float p = Random.value;

        if (p > 0.9)
        {
            Item clone = Instantiate(potion);
            clone.transform.position = transform.position;
        }else if (p < 0.95)
        {
            Debug.Log("Hat");
            Item clone = Instantiate(hat);
            clone.transform.position = transform.position;
            // item = new Sword();
        }
        else
        {
           
        }
    }

    override protected void AdditionalEffects()
    {
        PopItem();
    }

    void Awake()
    {
        Destroy(gameObject, 12f);
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
