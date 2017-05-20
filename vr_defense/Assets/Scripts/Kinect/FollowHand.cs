using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour {

    public GameObject Hand;
    public GameObject Bone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Hand.transform.position;
        transform.rotation = Bone.transform.rotation;
	}
}
