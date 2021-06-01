using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidwayScript : MonoBehaviour
{
    Player prophet;

    SpriteRenderer S_Renderer;

    // Start is called before the first frame update
    void Start()
    {
        prophet = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        S_Renderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player_Character")
        {
            prophet.updateSpawn(transform.position + new Vector3(0.5f, -0.25f, 0));
            this.GetComponent<BoxCollider2D>().enabled = false;
            S_Renderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }
}
