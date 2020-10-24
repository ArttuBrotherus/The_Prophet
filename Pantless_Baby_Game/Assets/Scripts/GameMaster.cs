using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

    GameObject[] invisibleInGame;

    public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
	}

    private void Awake()
    {
        invisibleInGame = GameObject.FindGameObjectsWithTag("HiddenIn-Game");
        foreach (GameObject hideItem in invisibleInGame)
        {
            hideItem.GetComponent<SpriteRenderer>().enabled = false;
        }

        Application.targetFrameRate = 60;
    }

}