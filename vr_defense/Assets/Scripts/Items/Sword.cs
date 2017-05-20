﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Item {

    private int nb_swords;

	// Use this for initialization
	void Start () {
        foreach(Transform child in transform){
            child.gameObject.SetActive(false);
        }
        nb_swords = transform.childCount;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void generateSword()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(Random.Range(0, nb_swords - 1)).gameObject.SetActive(true);
    }
}
