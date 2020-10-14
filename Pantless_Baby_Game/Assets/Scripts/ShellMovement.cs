using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMovement : MonoBehaviour
{

    Sprite redRectangle;
    
    float maxTravelDistance = 4;
    float currentTravelDistance = 0;

    //(used for index)
    //0 when going up
    //1 when going down
    int travelNumber = 0;

    delegate void proceedTravel();

    // Start is called before the first frame update
    void Start()
    {
        List<proceedTravel> travelOn = new List<proceedTravel>();
        travelOn.Add(goUp); //0
        travelOn.Add(goDown); //1
    }

    //rope_particles = Enumerable.Range(1, RopeParticleAmount).Select(_ => Instantiate(Rope_Particle, center_of_target.position, Quaternion.identity)).ToArray();

    // Update is called once per frame
    void Update()
    {
        //travelOn[travelNumber]();
    }

    void goUp(){
        transform.position.y += Time.deltaTime;
        currentTravelDistance += Time.deltaTime;
        if(currentTravelDistance > maxTravelDistance){
            travelNumber = 1;
        }
    }

    void goDown(){
        transform.position.y -= Time.deltaTime * 2;
        currentTravelDistance -= Time.deltaTime * 2;
        //transform.position.y, currentTravelDistance -= Time.deltaTime * 2;
        if(currentTravelDistance < 0f){
            travelNumber = 0;
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
