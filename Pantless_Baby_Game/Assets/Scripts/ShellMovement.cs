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

    float shellSpeed = 1.2f;

    //1 when going
    //- 1 when returning
    int returning = 1;

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

            yield return new WaitForSeconds(1f / shellSpeed);
        }     
    }

    void newDirection()
    {

        var shellPos = transform.position;

        if(returning == 1)
        {
            route.Add(new Vector2(0, 1));

            if (route.Count > 3)
            {
                returning = -1;
                shellSpeed *= 2;
            }
        }
        else
        {
            //subtract items

            route.Remove(route[route.Count - 1]);

            if (route.Count < 2)
            {
                returning = 1;
                shellSpeed /= 2;
            }
        }

    }

    //rope_particles = Enumerable.Range(1, RopeParticleAmount).Select(_ => Instantiate(Rope_Particle, center_of_target.position, Quaternion.identity)).ToArray();

    // Update is called once per frame
    void Update()
    {
        moveShell();
    }

    void moveShell(){

        transform.position = newShellPos();

        currentTravelDistance += shellSpeed * Time.deltaTime;
    }

    Vector2 newShellPos()
    {
        var shellPos = transform.position;
        var crntItem = route[route.Count - 1];

        float[] coordinates = new float[2];
        for(int i = 0; i < 2; i++)
        {
            var itemAndShell = i == 0 ? shellPos.x + crntItem.x : shellPos.y + crntItem.y;
            coordinates[i] = itemAndShell * Time.deltaTime * shellSpeed * returning;
        }
        return new Vector2(coordinates[0], coordinates[1]);
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
