using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kangoroo : MonoBehaviour
{

    public float jumpVelX;
    public float jumpVelY;
    public float waitTime;

    int groundedCount = 0;
    Rigidbody2D roo;

    ContactPoint2D[] contacts = new ContactPoint2D[100];

    GameObject rooWeapon;

    // Start is called before the first frame update
    void Start()
    {
        roo = GetComponent<Rigidbody2D>();
        if(jumpVelX == 0)
        {
            jumpVelX = 4f;
            jumpVelY = 5f;
        }
        if(waitTime == 0)
        {
            waitTime = 1f;
        }

        rooWeapon = this.transform.GetChild(0).gameObject;
        rooWeapon.SetActive(false);
    }

    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(waitTime);
        roo.velocity = new Vector2(jumpVelX * this.transform.lossyScale.x, jumpVelY);
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
                roo.velocity = new Vector2(jumpVelX * this.transform.localScale.x, roo.velocity.y);
            }
            else
            {
                roo.velocity = new Vector2(0, 0);
            }

            rooWeapon.SetActive(false);

            StartCoroutine("Jump");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (relevantCollider(collision))
        {
            groundedCount--;
            StopCoroutine("Jump");
            rooWeapon.SetActive(true);
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
