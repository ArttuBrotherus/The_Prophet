using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveTreshold = 0.1f;

    float offsetZ;
    Vector3 lastTargetPosition;
    Vector3 currentVelocity;
    Vector3 lookAheadPos;

    float remove_following_time = 0;

	// Use this for initialization
	void Start () {
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {

        // update lookahead pos only if accelerating or changed direction
        float xMoveDelta = (target.position - lastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) >
            lookAheadMoveTreshold;

        if (updateLookAheadTarget)
        {
            lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        } else {
            lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero,
                Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

        transform.position = newPos;

        lastTargetPosition = target.position;
	}

    public void remove_following()
    {
        remove_following_time = Time.fixedTime;
        Debug.Log("remFol");
        //after 1 second, the camera should focus on the point, the player character has fallen at present.
    }
}
