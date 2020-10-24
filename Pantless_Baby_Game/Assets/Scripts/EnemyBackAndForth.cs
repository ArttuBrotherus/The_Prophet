using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackAndForth : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;   
    Rigidbody2D body;

    int goingRight = 1;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(0.75f * goingRight, body.velocity.y);
    }

    public void Turn()
    {
        if (spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
            goingRight = 1;
        }
        else
        {
            spriteRenderer.flipX = true;
            goingRight = -1;
        }
    }
}
