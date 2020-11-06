using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    private GamePoints gm;
    private bool dead = false;
    float death_time;

    Component[] colliders;

    private void Start()
    {
        
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GamePoints>();
    }

    void Update() {
        if (dead)
        {
            transform.Translate(0,7 * Time.deltaTime, 0);
            if(Time.fixedTime > death_time + 1.5f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Currency"))
        {
            Destroy(collision.gameObject);
            gm.points += 1;
        }
    }

    public void Die (){
        if (dead) return;
        dead = true;
        Debug.Log("DEATH");
        this.GetComponent<CharacterController>().enabled = false;
        transform.Rotate(0,0,180f);
        this.GetComponent<Collider2D>().enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;
        death_time = Time.fixedTime;

        colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }
    }

}
