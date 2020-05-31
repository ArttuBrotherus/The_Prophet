using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    bool goingUp = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform position;

    public GameObject Rope_Particle;

    bool normal_movement = true;

    GameObject[] rope_particles;

    Vector3 pearl_block;

    int RopeParticleAmount = 20;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        position = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        if(normal_movement){
            NormalMovement();
        }else{
            LevitationMovement();
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

        normal_movement = false;

        pearl_block = new Vector3(position.x + 0.5f, position.y - 0.5f, 1);
	}
    
    void LevitationMovement(){
        for(int particle = 0; particle < rope_particles.Length; particle++){
            float distance_progress = (particle + 1) / Convert.ToSingle(RopeParticleAmount);
            float x = pearl_block.x * distance_progress + (1 - distance_progress) * this.gameObject.transform.position.x;
            float y = pearl_block.y * distance_progress + (1 - distance_progress) * this.gameObject.transform.position.y;
            // 2. set the desired position for the rope particle
            rope_particles[particle].transform.position = new Vector3(x, y, 1);

            //SEE ALSO: Quaternion and Matrix4x4 classes for rotation

            this.gameObject.transform.RotateAround(pearl_block, Vector3.zero, 10 * Time.deltaTime);
        }        
    }

    void NormalMovement(){
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

}