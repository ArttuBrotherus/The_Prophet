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

    int Step = 0;
    float travelledDistance = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var currentStep = track[Step];
        var x = currentStep.startPoint.x + currentStep.direction.x * travelledDistance;
        var y = currentStep.startPoint.y + currentStep.direction.y * travelledDistance;
        transform.position = new Vector2(x, y);
        travelledDistance += Time.deltaTime;

        if(travelledDistance > currentStep.distance)
        {
            Step++;
            if (Step > track.Length - 1) Step = 0;
            travelledDistance = 0;
        }
    }

}
