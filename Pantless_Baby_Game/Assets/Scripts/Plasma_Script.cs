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

    public MovementStep[] track;
    private SpriteRenderer spriteRenderer;

    int Step = 0;
    float travelledDistance = 0;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //The spikes of the plasma trap point towards the direction the trap is headed
        //The sprite needs to be flipped if the trap is currently going
        //left

        flipSprite();

        if (speed == 0f) speed = 1f;

    }

    MovementStep CurrentStep => track[Step];
    Vector2 CurrentDirection => CurrentStep.direction;

    // Update is called once per frame
    void Update()
    {

        //Add and subtract 0.5f because the sprite pivot is in the center so that
        //the image in question can be flipped when changing directions
        var x = CurrentStep.startPoint.x + CurrentDirection.x * travelledDistance + 0.5f;
        var y = CurrentStep.startPoint.y + CurrentDirection.y * travelledDistance - 0.5f;
        transform.position = new Vector2(x, y);
        travelledDistance += Time.deltaTime * speed;

        if(travelledDistance > CurrentStep.distance)
        {
            Step++;

            if (Step > track.Length - 1) Step = 0;
            flipSprite();
            travelledDistance = 0;
        }
    }

    void flipSprite()
    {
        //I feel like the statements below should be reversed but
        //if it's not bugged (it isn't), don't fix it
        if (CurrentDirection.x == 1)
        {
            spriteRenderer.flipX = true;
        }
        else if(CurrentDirection.x == -1)
        {
            spriteRenderer.flipX = false;
        }
    }

}
