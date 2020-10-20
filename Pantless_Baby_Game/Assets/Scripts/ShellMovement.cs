using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMovement : MonoBehaviour
{

    private IEnumerator coroutine;

    public Sprite redRectangle;
    
    float currentTravelDistance = 0;

    List<Vector2> route = new List<Vector2>();

    const float shellSpeedBase = 1.2f;
    float shellSpeed = shellSpeedBase;

    //- 1 when going
    //1 when returning
    int returning = -1;

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
        if (returning == -1)
        {  
            if (route.Count == 4)
            {
                returning = 1;
                shellSpeed = shellSpeedBase * 2;
            } else
            {
                route.Add(seekPlayer());
            }
        }
        else
        {
            if (route.Count == 1)
            {
                transform.localPosition = new Vector3(0, 0.75f, 0);

                returning = -1;
                shellSpeed = shellSpeedBase;
            }

            if (route.Count != 1) route.Remove(route[route.Count - 1]);
        }
        //Debug.Log(string.Format("New direction: returning: {0}   route count: {1}", returning, route.Count));

    }

    Vector2 seekPlayer()
    {
        var shellPos = transform.position;
        var playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        //down = Vector2(0, -1)
        //right = Vector2(1, 0)

        if(Mathf.Abs(shellPos.x - playerPos.x) > Mathf.Abs(shellPos.y - playerPos.y))
        {
            return shellPos.x > playerPos.x ? Vector2.left : Vector2.right;
        }
        else
        {
            return shellPos.y > playerPos.y ? Vector2.down : Vector2.up;
        }
    }

    //rope_particles = Enumerable.Range(1, RopeParticleAmount).Select(_ => Instantiate(Rope_Particle, center_of_target.position, Quaternion.identity)).ToArray();

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 180f * Time.deltaTime);

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

        var x = shellPos.x + crntItem.x * Time.deltaTime * shellSpeed * -returning;
        var y = shellPos.y + crntItem.y * Time.deltaTime * shellSpeed * -returning;

        return new Vector2(x, y);
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
