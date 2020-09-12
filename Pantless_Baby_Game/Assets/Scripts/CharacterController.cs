﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public bool normal_movement = true;
    public bool onGround = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform position;

    public GameObject Rope_Particle;

    GameObject[] rope_particles;

    Transform pearl_block;

    int RopeParticleAmount = 20;

    float orbiting_number;

    bool touchingBP = false;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        position = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        if (normal_movement){
            NormalMovement();
        }else{
            LevitationMovement();
        }
    }

    /*
    public void StartRotation(Vector3 position)
    {
        // a = ..
        // a[index] =
        
        // "Classic" for-loop way of initiating 10 rope perticles
        // rope_particles = new GameObject[10];
        // for(int particle = 0; particle < rope_particles.Length; particle++){
        //    rope_particles[particle] = Instantiate(Rope_Particle, position, Quaternion.identity);
        // }

        // *Better* More modern *functional programming* way of initiating 10 rope particles:
        // This avoids (a) creation of array with "new", (b) error-prone manual manipulation of index-variable and
        // (c) modifying the array contents.
        rope_particles = Enumerable.Range(1,RopeParticleAmount).Select(_ => Instantiate(Rope_Particle, position, Quaternion.identity)).ToArray();

    */

    public bool IsMovementNormal()
    {
        return normal_movement;
    }

    void StopRotation()
    {
        normal_movement = true;

        transform.parent = null;

        GetComponent<DistanceJoint2D>().enabled = false;

        foreach(GameObject particle in rope_particles)
        {
            Destroy(particle);
        }

        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
    
    public void StartRotation(Transform center_of_target, float orbiting_direction_number)
    {
        normal_movement = false;

        pearl_block = center_of_target;

        transform.parent = pearl_block;

        var block_joint = GetComponent<DistanceJoint2D>();
        block_joint.enabled = true;
        block_joint.connectedAnchor = center_of_target.position;

        GetComponent<Rigidbody2D>().gravityScale = 0;

        rope_particles = Enumerable.Range(1, RopeParticleAmount).Select(_ => Instantiate(Rope_Particle, center_of_target.position, Quaternion.identity)).ToArray();

        orbiting_number = orbiting_direction_number;
    }

    void LevitationMovement(){

        for (int particle = 0; particle < rope_particles.Length; particle++)
        {
            float distance_progress = (particle + 1) / Convert.ToSingle(RopeParticleAmount);
            float x = pearl_block.position.x * distance_progress + (1 - distance_progress) * this.gameObject.transform.position.x;
            float y = pearl_block.position.y * distance_progress + (1 - distance_progress) * this.gameObject.transform.position.y;
            // 2. set the desired position for the rope particle
            rope_particles[particle].transform.position = new Vector3(x, y, 1);
        }

        pearl_block.Rotate(0, 0, orbiting_number * (-100f) * Time.deltaTime, Space.Self);
        transform.Rotate(0, 0, orbiting_number * 100f * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            StopRotation();
            Jumping(GetComponent<Rigidbody2D>());
        }
    }

    void NormalMovement(){
        var body = GetComponent<Rigidbody2D>();

        var movement = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(movement * 5.0f, body.velocity.y);

        if (Input.GetButtonDown("Jump") && onGround)
        {
            //transform.position = new Vector3(- 2, - 13);           
            if (touchingBP)
            {
                if(Input.GetAxis("Vertical") < 0)
                {
                    var BP = GameObject.FindGameObjectsWithTag("BP");
                    Debug.Log("Going down");
                    //BP.EffectorReversal();
                    return;
                }
            }
            Jumping(body);
        }

        //var movement = Input.GetAxis("Horizontal");

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("OnCollisionEnter, " + collision.collider.name + ", " + collision.gameObject + ", " + collision.otherCollider.name);
        if (!normal_movement)
        {
            StopRotation();
        }
        if (collision.otherCollider.name != "Player_Character" && collision.otherCollider.name != "Feet")
        {
            return;
        }
        onGround = true;
        if (collision.gameObject.CompareTag("BP"))
        {
            touchingBP = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("OnCollisionExit, " + collision.collider.name + ", " + collision.gameObject + ", " + collision.otherCollider.name);
        if(collision.otherCollider.name != "Player_Character" && collision.otherCollider.name != "Feet")
        {
            return;
        }
        onGround = false;
    }

    void Jumping(Rigidbody2D body)
    {
        body.velocity = new Vector2(body.velocity.x, jumpTakeOffSpeed);
    }

}