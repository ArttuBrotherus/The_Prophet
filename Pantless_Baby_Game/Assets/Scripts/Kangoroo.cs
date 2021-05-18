using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kangoroo : MonoBehaviour
{

    public Vector2 jumpVel;

    // Start is called before the first frame update
    void Start()
    {

        if(jumpVel == new Vector2 (0,0))
        {
            //change the two values to default

            jumpVel = new Vector2(1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
