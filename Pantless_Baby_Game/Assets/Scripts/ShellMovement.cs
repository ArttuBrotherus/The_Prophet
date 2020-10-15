using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMovement : MonoBehaviour
{

    private IEnumerator coroutine;

    public Sprite redRectangle;
    
    float maxTravelDistance = 4;
    float currentTravelDistance = 0;

    //List<SpawnRocksMethod> spawnRocks = new List<SpawnRocksMethod>();

    List<Vector2> route = new List<Vector2>();

    const float shellSpeed = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        coroutine = addNewDirection();
        StartCoroutine(coroutine);
    }

    private IEnumerator addNewDirection()
    {
        while (true)
        {
            newDirection();

            //shellspeed is const, for now
            yield return new WaitForSeconds(1f / shellSpeed);
        }     
    }

    bool dir;

    void newDirection()
    {
        var shellPos = transform.position;
        if(dir)
        {
            route.Add(new Vector2(- 1, 0));
            dir = false;
            Debug.Log("New direction! If!");
        }
        else
        {
            route.Add(new Vector2(0, - 1));
            dir = true;
            Debug.Log("New direction! Else!");
        }
    }

    //rope_particles = Enumerable.Range(1, RopeParticleAmount).Select(_ => Instantiate(Rope_Particle, center_of_target.position, Quaternion.identity)).ToArray();

    // Update is called once per frame
    void Update()
    {
        moveShell();
    }

    void moveShell(){

        var shellPos = transform.position;
        var crntItem = route[route.Count - 1];

        transform.position = new Vector2(shellPos.x + crntItem.x * Time.deltaTime * shellSpeed,

            shellPos.y + crntItem.y * Time.deltaTime * shellSpeed);

        currentTravelDistance += shellSpeed * Time.deltaTime;

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
