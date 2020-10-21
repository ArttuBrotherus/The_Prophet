using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnemyScript : MonoBehaviour
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
        if (target.tag == "Enemy")
        {
            var enemy = target.GetComponent<EnemyBackAndForth>();
            enemy.Turn();
            Debug.Log("Trigger");
        }
    }
}
