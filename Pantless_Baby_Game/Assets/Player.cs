﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private GamePoints gm;
    private bool dead = false;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GamePoints>();
    }

    void Update() {
        if (dead)
        {
            transform.Translate(0,7 * Time.deltaTime, 0);
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
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().remove_following();
    }

}
