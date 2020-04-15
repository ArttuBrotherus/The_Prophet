using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teeth_wheel_script : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.Rotate(0,0,-360f * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        var target = col.gameObject;
        if (target.tag == "Player") {
            var player = target.GetComponent<Player>();
            player.Die();
        }
    }
}
