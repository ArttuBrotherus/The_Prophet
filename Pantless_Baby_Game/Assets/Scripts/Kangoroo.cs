using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kangoroo : MonoBehaviour
{

    public float jumpVelX;
    public float jumpVelY;

    public int groundedCount = 0;
    Rigidbody2D roo;

    ContactPoint2D[] contacts = new ContactPoint2D[100];

    // Start is called before the first frame update
    void Start()
    {
        roo = GetComponent<Rigidbody2D>();
        if(jumpVelX == 0)
        {
            jumpVelX = 2f;
            jumpVelY = 2.5f;
        }
    }

    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(1f);
        roo.velocity = new Vector2(jumpVelX * this.transform.lossyScale.x, jumpVelY) * 2;
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

            var contactNo = collision.GetContacts(contacts);
            var hitWall = false;

            for (int i = 0; i < contactNo; i++)
            {
                if(contacts[i].point.y > this.transform.position.y - 0.4f)
                {
                    //Wall                    
                    hitWall = true;
                }
            }
            if (hitWall)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x * -1f, 1f, 1f);
                roo.velocity = new Vector2(jumpVelX * this.transform.lossyScale.x, roo.velocity.y) * 2;
            }
            else
            {
                roo.velocity = new Vector2(0, 0);
            }
            StartCoroutine("Jump");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (relevantCollider(collision))
        {
            groundedCount--;
            StopCoroutine("Jump");
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
