using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour {

    public float lifetime = 4.0f;

    void Awake()
    {
        Destroy(gameObject, lifetime);
    }
}
