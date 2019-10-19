using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Collision : MonoBehaviour {

    BoxCollider2D thisColl;
    Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        thisColl = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
