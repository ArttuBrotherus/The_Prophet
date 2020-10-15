using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMovement : MonoBehaviour
{

    Sprite redRectangle;
    
    float maxTravelDistance = 4;
    float currentTravelDistance = 0;

    //1 when going up
    //- 1 when going down
    int direction = 1;
    const float shellSpeed = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    //rope_particles = Enumerable.Range(1, RopeParticleAmount).Select(_ => Instantiate(Rope_Particle, center_of_target.position, Quaternion.identity)).ToArray();

    // Update is called once per frame
    void Update()
    {
        moveShell();
    }

    void moveShell(){
        var shellPos = transform.position;

        transform.position = new Vector2(shellPos.x, shellPos.y + Time.deltaTime * shellSpeed * direction);

        currentTravelDistance += direction * Time.deltaTime;

        if (currentTravelDistance > maxTravelDistance || currentTravelDistance < 0f)
        {
            direction = -direction;
        }

    }

    //---------------------------------------------------

    /*

    // define a delegate type to match the required method signature
     delegate void SpawnRocksMethod();
     void CreateList()
     {
         // create a list of delegate objects as placeholders for the methods.
         // note the methods must all be of type void with no parameters
         // that is they must all have the same signature.
         List<SpawnRocksMethod> spawnRocks = new List<SpawnRocksMethod>();
         spawnRocks.Add(spawnRocks1);
         spawnRocks.Add(spawnRocks2);
         // call a method ...
         spawnRocks[0]();
     }
     //confirmed working
     void spawnRocks1()
     {
         Instantiate something;
     }
     void spawnRocks2()
     {
         Instantiate something;
     }

    */
}
