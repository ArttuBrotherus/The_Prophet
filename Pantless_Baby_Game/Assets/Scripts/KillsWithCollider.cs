using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillsWithCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var target = col.gameObject;
        if (target.tag == "Player")
        {
            var player = target.GetComponent<Player>();
            player.Die();
        }
    }
}
