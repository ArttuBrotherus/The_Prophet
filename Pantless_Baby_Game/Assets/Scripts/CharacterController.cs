using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float characterSpeed;
    public float jumpTakeOffSpeed;
    public bool normal_movement = true;
    public int groundedCount = 0;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform position;

    public GameObject Rope_Particle;

    GameObject[] rope_particles;

    Transform pearl_block;
    float blockAngle;
    const int RopeParticleAmount = 20;
    float blockDistance;

    float orbiting_direction; // orbiting_direction = 1 when orbiting clockwise (when rotating pearl block)

    //used in determining whether character can drop off from a platform
    bool touchingBP = false;

    public GameObject feet;

    public AudioClip jumpSound;
    AudioPlayback sndPlayer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        position = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        sndPlayer = GameObject.FindWithTag("Gramophone").GetComponent<AudioPlayback>();
    }

    // Update is called once per frame
    void Update() {
        if (normal_movement){
            NormalMovement();
        }else{
            LevitationMovement();
        }
    }

    public bool IsMovementNormal()
    {
        //We also check whether the player is in mid-air. This is to avoid forcing
        //the player character inside a collider.
        return (normal_movement && groundedCount == 0);
    }

    //stop rotation upon death:
    public void rotationDeath()
    {
        StopRotation();
    }

    void StopRotation()
    {
        normal_movement = true;

        if (rope_particles != null) {
            foreach (GameObject particle in rope_particles) Destroy(particle);

            //Orbiting has ended so the program changes the target block sprite
            //back to normal
            var target_script = GameObject.FindWithTag("Target").GetComponent<Target>();
            target_script.changeSpriteBack();
        }

        GetComponent<Rigidbody2D>().gravityScale = 1;

        feet.layer = 11;

        rope_particles = null;
    }

    public void StartRotation(Transform center_of_target, float orbiting_direction_number)
    {

        normal_movement = false;

        Debug.Assert(center_of_target != null, "Center of target is null");

        pearl_block = center_of_target;

        GetComponent<Rigidbody2D>().gravityScale = 0;

        rope_particles = Enumerable.Range(1, RopeParticleAmount).Select(_ => Instantiate(Rope_Particle, center_of_target.position, Quaternion.identity)).ToArray();

        orbiting_direction = orbiting_direction_number;

        var deltaX = transform.position.x - pearl_block.position.x;
        var deltaY = transform.position.y - pearl_block.position.y;

        blockAngle = correctBlockAngle(deltaX, deltaY);

        blockDistance = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);

        //Feet layer changed so that player doesn't interact
        //with BP mid-orbit 
        feet.layer = 8;
    }

    float correctBlockAngle(float deltaX, float deltaY)
    {
        var quandrantAngle = Mathf.Atan2(Mathf.Abs(deltaY), Mathf.Abs(deltaX));
        if (deltaX > 0 && deltaY > 0) return quandrantAngle; // oik ylä
        if (deltaX > 0 && deltaY < 0) return -quandrantAngle + Mathf.PI * 2.0f; // oik ala
        if (deltaX < 0 && deltaY < 0) return quandrantAngle + Mathf.PI * 1.0f; // vase ala
        return -quandrantAngle + Mathf.PI * 1.0f; // vasen ylä
    }

    void LevitationMovement(){

        //Orbiting should be clockwise if orbiting_number is 1, how come this, possibly,
        //isn't the case?
        blockAngle += Time.deltaTime * 2 * orbiting_direction;

        //Change blockDistance mid-flight:
        if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.W))){
            blockDistance += Time.deltaTime * 1.5f;
        }else if(Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.S))){
            blockDistance -= Time.deltaTime * 1.5f;
        }

        float ukkoX = Mathf.Cos(blockAngle) * blockDistance + pearl_block.position.x;
        float ukkoY = Mathf.Sin(blockAngle) * blockDistance + pearl_block.position.y;

        //Set the character's next position:
        this.gameObject.transform.position = new Vector3(ukkoX, ukkoY);

        for (int particle = 0; particle < rope_particles.Length; particle++)
        {
            float distance_progress = (particle + 1) / Convert.ToSingle(RopeParticleAmount);
            // 2. set the desired position for the rope particle, use vector calculation
            rope_particles[particle].transform.position = 
                pearl_block.position * distance_progress + 
                (1 - distance_progress) * gameObject.transform.position;
        }

        if (Input.GetButtonDown("Jump"))
        {
            StopRotation();
            Jumping();
        }
    }

    void NormalMovement(){
        var body = GetComponent<Rigidbody2D>();

        var movement = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(movement * characterSpeed, body.velocity.y);

        if (Input.GetButtonDown("Jump") && groundedCount > 0)
        {         
            Jumping();
        }

        if (Input.GetKeyDown(KeyCode.S) && touchingBP)
        {
            //If we're on top of a platform, press S so we'll fall through the platform

            // Getting the script of BP, executing its public function
            var effector = feet.GetComponent<OneWayPlatformEffectorReversal>();
            effector.EffectorReversal();
            return;
        }

        if (body.velocity.x != 0)
        {
            animator.SetBool("walking", true);
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
            animator.SetBool("walking", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!normal_movement) StopRotation();

        // Need to check both Player_Character and Feet colliders since Feet is only
        // used with Blue Platform.
        if (collision.otherCollider.name == "Feet") {
            groundedCount++;
            if (collision.gameObject.CompareTag("BP")) touchingBP = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {    
        if(collision.otherCollider.name == "Feet") {
            groundedCount--;
        }
    }

    void Jumping()
    {
        sndPlayer.oneSound(jumpSound, 0.075f);
        var body = GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(body.velocity.x, jumpTakeOffSpeed);
    }

}