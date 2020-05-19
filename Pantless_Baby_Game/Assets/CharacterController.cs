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
    private Transform position;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        position = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        var body = GetComponent<Rigidbody2D>();

        var movement = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(movement * 5.0f, body.velocity.y);

        var feet = this.transform.GetChild(0);
        var floor_detected = isFloorDetected(this.GetComponent<BoxCollider2D>()) ||
                isFloorDetected(feet.GetComponent<BoxCollider2D>());

        if (Input.GetButtonDown("Jump") && floor_detected)
        {
            body.velocity = new Vector2( body.velocity.x, jumpTakeOffSpeed);  
            changePlatformTriggerState(makeBlocking: false);
            goingUp = true;
            floor_detected = false;
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
            changePlatformTriggerState(makeBlocking: true);
            goingUp = false;
        }
    }

    void changePlatformTriggerState (bool makeBlocking)
    {
        GameObject[] bluePlatforms = GameObject.FindGameObjectsWithTag("BP");
        foreach (GameObject platform in bluePlatforms)
        {
            var touchingPlayer = platform.GetComponent<BoxCollider2D>().IsTouching(GetComponent<Collider2D>());
            if(makeBlocking){
                if(!touchingPlayer){
                    platform.GetComponent<BoxCollider2D>().isTrigger = false;
				}
			} else {
                platform.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
    }

    bool isFloorDetected(BoxCollider2D GOCollider)
    {
        var colliders = new Collider2D[100];
        var colliderNumber = GOCollider.GetContacts(colliders);
        for (int collider = 0; collider < colliderNumber; collider++)
        {
            if (colliders[collider].CompareTag("Floor") || colliders[collider].CompareTag("BP"))
            {
                return true;
            }
        }
        return false;

    }
}