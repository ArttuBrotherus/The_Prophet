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

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //The spikes of the plasma trap point towards the direction the trap is headed
        //The sprite needs to be flipped if the trap is currently going
        //left

        flipSprite();

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
        travelledDistance += Time.deltaTime * 1.0f;

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
        if (CurrentDirection.x == 1)
        {
            //The trap is going to the right, no need to change the sprite from normal
            spriteRenderer.flipX = false;
        }
        else if(CurrentDirection.x == -1)
        {
            //Going to the left, have the sprite flipped
            spriteRenderer.flipX = true;
        }
    }

}
