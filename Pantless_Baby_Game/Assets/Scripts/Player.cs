using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    private GamePoints gm;
    private bool dead = false;
    float death_time;

    Component[] colliders;

    CharacterController contr;
    Vector3 spawnHere;

    public AudioClip defeatSound;
    AudioPlayback sndPlayer;

    private void Start()
    {
        
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GamePoints>();
        contr = this.GetComponent<CharacterController>();
        spawnHere = transform.position;
        sndPlayer = GameObject.FindWithTag("Gramophone").GetComponent<AudioPlayback>();
    }

    public void updateSpawn(Vector3 newCoord)
    //see above: new coordinates to act as the spawn-point
    {
        spawnHere = newCoord;
    }

    void Update() {
        if (dead)
        {
            transform.Translate(0,7 * Time.deltaTime, 0);
            if(Time.fixedTime > death_time + 1.5f)
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Respawn();               
            }
        }
    }

    void Respawn()
    {
        transform.position = spawnHere;

        dead = false;

        //remember, THIS is the way to rotate
        transform.Rotate(0, 0, 180f);

        this.GetComponent<CharacterController>().enabled = true;
        this.GetComponent<Collider2D>().enabled = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = true;

        colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = true;
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
        sndPlayer.oneSound(defeatSound, 0.31f);
        dead = true;
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

        //stop rotation upon death
        contr.rotationDeath();
    }

}
