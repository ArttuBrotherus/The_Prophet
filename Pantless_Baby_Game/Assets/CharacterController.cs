using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    bool goingUp = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        var body = GetComponent<Rigidbody2D>();

        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump")) // && grounded
        {
            Debug.Log("Jump!");
            body.velocity = new Vector2( body.velocity.x, jumpTakeOffSpeed);  
            changePlatformTriggerState(true);
            goingUp = true;
        }

        if (body.velocity.x != 0)
        {
            GetComponent<Animator>().enabled = true;
            if (body.velocity.x > 0.01f)
            {
                spriteRenderer.flipX = false;
            }
            else if (body.velocity.x < -0.01f)
            {
                spriteRenderer.flipX = true;
            }

        }
        else
        {
            GetComponent<Animator>().enabled = false;
        }
        // TODO. We should in following also check that we are currently not touching any platform
        if (goingUp == true && body.velocity.y < 0)
        {
            changePlatformTriggerState(false);
            goingUp = false;
        }
    }

    void changePlatformTriggerState (bool triggerState)
    {
        Debug.Log("changePlatformTS " + triggerState.ToString());
        GameObject[] bluePlatforms = GameObject.FindGameObjectsWithTag("BP");
        foreach (GameObject platform in bluePlatforms)
        {
            platform.GetComponent<BoxCollider2D>().isTrigger = triggerState;
        }
    }
}