﻿using System.Collections;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "ilkka")
        {
            Debug.Log("Ilkka");
            var physics = col.GetComponent<PhysicsScript>();
            physics.StopIfGoingDown();
        }

        return;


        BoxCollider2D parentCollider = gameObject.GetComponentInParent(typeof(BoxCollider2D)) as BoxCollider2D;

        
        Debug.Log(rb.velocity);
        if (rb.velocity.y > 0)
        {
            parentCollider.enabled = false;
            Debug.Log("frodi");
        }
    }
}
