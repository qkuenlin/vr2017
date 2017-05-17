using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBall : SpellProjectile
{
    float chargeStart;
    bool charging = false;

    float maxCharge = 6.0f;
    float radiusSpeed = 0.5f;//speed at which the radius increase (radius = radiusSpeed*chargeTime)

    public SphereCollider sphereCollider;

    public void ExplosionDamage()
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

    public float targetRadius()
    {
        float chargeTime = Mathf.Min(Time.time - chargeStart, maxCharge);
        if (charging)
        {
            return Mathf.Max(0.0f, chargeTime * radiusSpeed);
        }
        return 0.0f;
    }

    override protected void OnTriggerEnter(Collider col)
    {

    }

    public void Charge(Vector3 target)
    {
        if (!charging)
        {
            charging = true;
            chargeStart = Time.time;//time since start of game
        }
        sphereCollider.transform.position = target;
        sphereCollider.transform.localScale = new Vector3(1f, 1f, 1f);
        float radius = targetRadius();
        sphereCollider.transform.localScale *= radius;
        sphereCollider.radius = radius;

        transform.localScale.Set(radius, radius, radius);
        transform.position = target;

        Debug.Log(target + "   " + radius);

    }

    public void Release()
    {
        Debug.Log("released");
        ExplosionDamage();
        charging = false;
        sphereCollider.transform.localScale = new Vector3(0f, 0f, 0f);
        sphereCollider.radius = 0f;
        transform.localScale.Set(0f, 0f, 0f);
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
