using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {


    public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
	}

}