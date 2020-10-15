using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MovementStep
{
    [SerializeField] public Vector2 startPoint;
    [SerializeField] public Vector2 direction; //eg. [+1,0]
    [SerializeField] public float distance;
}

public class Plasma_Script : MonoBehaviour
{

    bool goingRight;
    public MovementStep[] track;
    private SpriteRenderer spriteRenderer;

    int Step = 0;
    float travelledDistance = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //The spikes of the plasma trap point towards the direction the trap is headed
        //The sprite needs to be flipped if the trap is currently going
        //left

        goingRight = true;
        if(track[Step].direction.x == -1)
        {
            goingRight = false;
            flipSprite();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var currentStep = track[Step];

        //Add and subtract 0.5f because the sprite pivot is in the center so that
        //the image in question can be flipped when changing directions
        var x = currentStep.startPoint.x + currentStep.direction.x * travelledDistance + 0.5f;
        var y = currentStep.startPoint.y + currentStep.direction.y * travelledDistance - 0.5f;
        transform.position = new Vector2(x, y);
        travelledDistance += Time.deltaTime * 1.0f;

        if(travelledDistance > currentStep.distance)
        {
            var previousDirection = currentStep.direction.x;
            Step++;

            //Debug.Log(previousDirection + ", " + currentStep.direction.x);
            if(currentStep.direction.x != previousDirection)
            {
                goingRight = !goingRight;
                flipSprite();
            }

            if (Step > track.Length - 1) Step = 0;
            travelledDistance = 0;
        }
    }

    void flipSprite()
    {
        if (goingRight)
        {
            //The trap is going to the right, no need to change the sprite from normal
            spriteRenderer.flipX = false;
        }
        else
        {
            //Going to the left, have the sprite flipped
            spriteRenderer.flipX = true;
        }
    }

}
