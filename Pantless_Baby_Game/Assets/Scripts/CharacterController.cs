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
    int RopeParticleAmount = 20;
    float blockDistance;

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

    public bool IsMovementNormal()
    {
        //We also check whether the player is in mid-air. This is to avoid forcing
        //the player character inside a collider.
        return (normal_movement && groundedCount == 0);
    }

    void StopRotation()
    {
        Debug.Log("StopRotation");
        normal_movement = true;

        foreach(GameObject particle in rope_particles)
        {
            Destroy(particle);
        }

        GetComponent<Rigidbody2D>().gravityScale = 1;

        //Orbiting has ended so the program changes the target block sprite
        //back to normal
        var target_script = GameObject.FindWithTag("Target").GetComponent<Target>();
        target_script.changeSpriteBack();
    }

    public void StartRotation(Transform center_of_target, float orbiting_direction_number)
    {

        normal_movement = false;

        Debug.Assert(center_of_target != null, "Center of target is null");

        pearl_block = center_of_target;

        GetComponent<Rigidbody2D>().gravityScale = 0;

        rope_particles = Enumerable.Range(1, RopeParticleAmount).Select(_ => Instantiate(Rope_Particle, center_of_target.position, Quaternion.identity)).ToArray();

        orbiting_number = orbiting_direction_number;

        var deltaX = transform.position.x - pearl_block.position.x;
        var deltaY = transform.position.y - pearl_block.position.y;

        blockAngle = correctBlockAngle(deltaX, deltaY);

        blockDistance = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    float correctBlockAngle(float deltaX, float deltaY)
    {
        var quandrantAngle = Mathf.Atan2(Mathf.Abs(deltaY), Mathf.Abs(deltaX));
        if (deltaX > 0 && deltaY > 0) // oik ylä
        {
            return quandrantAngle;
        }
        else if (deltaX > 0 && deltaY < 0) // oik ala
        {
            return -quandrantAngle + Mathf.PI * 2.0f;
        }
        else if (deltaX < 0 && deltaY < 0) // vase ala
        {
            return quandrantAngle + Mathf.PI * 1.0f;
        }
        else // vasen ylä
        {
            return -quandrantAngle + Mathf.PI * 1.0f;
        }
    }

    void LevitationMovement(){

        //Orbiting should be clockwise if orbiting_number is 1, how come this, possibly,
        //isn't the case?
        blockAngle += Time.deltaTime * 2 * orbiting_number;

        //Change blockDistance mid-flight:
        if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.W))){
            blockDistance += Time.deltaTime * 1.25f;
        }else if(Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.S))){
            blockDistance -= Time.deltaTime * 1.25f;
        }

        float ukkoX = Mathf.Cos(blockAngle) * blockDistance + pearl_block.position.x;
        float ukkoY = Mathf.Sin(blockAngle) * blockDistance + pearl_block.position.y;

        //Set the character's next position:
        this.gameObject.transform.position = new Vector3(ukkoX, ukkoY);

        for (int particle = 0; particle < rope_particles.Length; particle++)
        {
            float distance_progress = (particle + 1) / Convert.ToSingle(RopeParticleAmount);
            float x = pearl_block.position.x * distance_progress + (1 - distance_progress) * this.gameObject.transform.position.x;
            float y = pearl_block.position.y * distance_progress + (1 - distance_progress) * this.gameObject.transform.position.y;
            // 2. set the desired position for the rope particle
            rope_particles[particle].transform.position = new Vector3(x, y, 1);
        }

        //orbiting_number = 1 when orbiting clockwise (when rotating pearl block)

        if (Input.GetButtonDown("Jump"))
        {
            StopRotation();
            Jumping(GetComponent<Rigidbody2D>());
        }
    }

    void NormalMovement(){
        var body = GetComponent<Rigidbody2D>();

        var movement = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(movement * characterSpeed, body.velocity.y);

        if (Input.GetButtonDown("Jump") && groundedCount > 0)
        {         
            if (touchingBP)
            {
                if(Input.GetKey(KeyCode.S))
                {
                    //If we're on top of a platform, press S and the jump button, we'll fall through the platform

                    // Getting the script of BP, executing its public function
                    var effector = GameObject.FindGameObjectWithTag("BP").GetComponent<OneWayPlatformEffectorReversal>();
                    effector.EffectorReversal();
                    return;
                }
            }
            Jumping(body);
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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!normal_movement)
        {
            StopRotation();
        }
        if (collision.otherCollider.name != "Player_Character" && collision.otherCollider.name != "Feet")
        {
            return;
        }

        groundedCount++;

        if (collision.gameObject.CompareTag("BP"))
        {
            touchingBP = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {    
        if(collision.otherCollider.name != "Player_Character" && collision.otherCollider.name != "Feet")
        {
            return;
        }

        groundedCount--;
    }

    void Jumping(Rigidbody2D body)
    {
        body.velocity = new Vector2(body.velocity.x, jumpTakeOffSpeed);
    }

}