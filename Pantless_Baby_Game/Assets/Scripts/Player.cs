using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    private GamePoints gm;
    private bool dead = false;
    float death_time;

    Dictionary<Tuple<int, int>, GameObject> blockLookup = new Dictionary<Tuple<int, int>, GameObject>();

    private void Start()
    {
        
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GamePoints>();

        /*
        
        GameObject[] allBlocs = null;
        foreach(var block in allBlocs)
        {
            var x = Convert.ToInt32( block.transform.position.x );
            var y = Convert.ToInt32(block.transform.position.y);
            blockLookup[Tuple.Create(x, y)] = block;
        }

        // Iterate row by row horizontally for "floors"
        for(int y= 1; y<100; y++) {
            for (int x = 1; x < 100; x++)
            {
                // Is the a block here?
                var isBlock = blockLookup.ContainsKey(Tuple.Create(x, y));
                var isBlockAbove = blockLookup.ContainsKey(Tuple.Create(x, y-1));
                var isBlockBelow = blockLookup.ContainsKey(Tuple.Create(x+1, y));
                if (isBlock && (!isBlockAbove || !isBlockBelow))
                {
                    // start a new floow or continue previous floor
                }

            }
        }

        // Iterate column by column vertically for "walls"
        for (int x = 1; y < 100; x++)
        {
            for (int y = 1; y < 100; x++)
            {
                // Is the a block here?
                var isBlock = blockLookup.ContainsKey(Tuple.Create(x, y));

            }
        }

    */

    }

    void Update() {
        if (dead)
        {
            transform.Translate(0,7 * Time.deltaTime, 0);
            if(Time.fixedTime > death_time + 1.5f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Currency"))
        {
            Destroy(collision.gameObject);
            gm.points += 1;
        }
    }

    public void Die (){
        if (dead) return;
        dead = true;
        Debug.Log("DEATH");
        this.GetComponent<CharacterController>().enabled = false;
        transform.Rotate(0,0,180f);
        this.GetComponent<Collider2D>().enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;
        death_time = Time.fixedTime;
    }

}
