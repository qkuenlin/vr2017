using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBall : SpellProjectile {
    float chargeStart;
    bool charging = false;

    const float maxCharge = 5.0f;

    public SphereCollider sphereCollider;

    void ExplosionDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(sphereCollider.transform.position, sphereCollider.radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Minion minion = (Minion)hitColliders[i].GetComponent("Minion");

            if (minion != null)
            {
                Collide(minion);
            }
            i++;
        }
    }

    float targetRadius()
    {
        float chargeTime = Time.time - chargeStart;
        if (charging && chargeTime < maxCharge)
        {
            return Mathf.Max(0.0f, chargeTime );
        }
        return 0.0f;
    }

    override protected void OnTriggerEnter(Collider col)
    {

    }

    void Charge(Vector3 target)
    {
        if (!charging)
        {
           // Debug.Log("Starting charge");
            charging = true;
            chargeStart = Time.time;//time since start of game
        }
        if (Time.time - chargeStart > maxCharge)
        {
            Release();
        }
        else
        {
            sphereCollider.transform.position = target;
            sphereCollider.transform.localScale = new Vector3(1f, 1f, 1f);
            sphereCollider.transform.localScale *= targetRadius();
            sphereCollider.radius = targetRadius();
        }
    }

    void Release()
    {
       // Debug.Log("realeased");
        ExplosionDamage();
        charging = false;
        sphereCollider.transform.localScale = new Vector3(0f, 0f, 0f);
        sphereCollider.radius = 0f;
        //effects and attacks
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 target = Input.mousePosition;
            target.Set(10f * (target.x / Screen.width - 0.5f), 0f, 7f + 10f * (target.y / Screen.height - 0.5f));
            Charge(target);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Release();
        }
    }
}
