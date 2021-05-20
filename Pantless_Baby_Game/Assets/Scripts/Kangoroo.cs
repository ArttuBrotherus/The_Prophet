using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kangoroo : MonoBehaviour
{

    public float jumpVelX;
    public float jumpVelY;

    private IEnumerator coroutine;

    public int groundedCount = 0;
    Rigidbody2D roo;

    // Start is called before the first frame update
    void Start()
    {
        roo = GetComponent<Rigidbody2D>();

        if(jumpVelX == 0)
        {
            jumpVelX = 3f;
            jumpVelY = 2.5f;
        }

        coroutine = Jump();
        StartCoroutine(coroutine);
    }

    private IEnumerator Jump()
    {

        yield return new WaitForSeconds(10f);

        while (true)
        {
            //yield return new WaitForSeconds(10f);

            roo.velocity = new Vector2(jumpVelX * this.transform.lossyScale.x, jumpVelY);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (relevantCollider(collision))
        {
            groundedCount++;
            StartCoroutine(coroutine);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (relevantCollider(collision))
        {
            groundedCount--;
            if(groundedCount == 0)
            {
                StopCoroutine(coroutine);
            }
        }
    }

    bool relevantCollider(Collision2D col)
    {
        if(col.collider.name != "Player_Character"
            && col.collider.name != "Feet")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
