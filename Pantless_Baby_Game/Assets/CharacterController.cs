using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : PhysicsScript {

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
        ComputeVelocity();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
            changePlatformTriggerState(true);
            goingUp = true;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        if (velocity.x > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (velocity.x < -0.01f)
        {
            spriteRenderer.flipX = true;
        }

        // TODO. We should in following also check that we are currently not touching any platform
        if (goingUp == true && velocity.y < 0)
        {
            changePlatformTriggerState(false);
            goingUp = false;
        }

        targetVelocity = move * maxSpeed;

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