using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MovementStep
{
    public Vector2 startPoint;
    public Vector2 direction; //eg. [+1,0]
    public float distance;
}

public class Plasma_Script : MonoBehaviour
{
    public static MovementStep[][] tracks =
        new MovementStep[][] {
            new MovementStep[]
            {
                new MovementStep{startPoint = new Vector2(67f, -21f), direction = new Vector2(0, -1), distance = 2f },
                new MovementStep{startPoint = new Vector2(67f, -23f), direction = new Vector2(0, 1), distance = 2f},
                new MovementStep{startPoint = new Vector2(67f, -21f), direction = new Vector2(-1, 0), distance = 4f},
                new MovementStep{startPoint = new Vector2(63f, -21f), direction = new Vector2(1, 0), distance = 4f},
            },
            new MovementStep[]
            {
                new MovementStep{startPoint = new Vector2(67f, -21f), direction = new Vector2(0, -1), distance = 2f },
                new MovementStep{startPoint = new Vector2(67f, -23f), direction = new Vector2(0, 1), distance = 2f},
                new MovementStep{startPoint = new Vector2(67f, -21f), direction = new Vector2(-1, 0), distance = 4f},
                new MovementStep{startPoint = new Vector2(63f, -21f), direction = new Vector2(1, 0), distance = 4f},
            }
        };

    // Dictionary<string, object>[] track2;  // alternative data structure

    public int trackIndex; 

    int Step = 0;
    float travelledDistance = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var track = tracks[trackIndex];
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
