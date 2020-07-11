using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

    GameObject[] Sup_Edges_L;
    public GameObject floor;
    public GameObject wall;

    Dictionary<Transform, GameObject> Physical_Materials = new Dictionary<Transform, GameObject>();

    public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
	}

    private void Start ()
    {
        CreatePhysicalBlocks();
    }

    void CreatePhysicalBlocks()
    {
        Sup_Edges_L = GameObject.FindGameObjectsWithTag("S_Edge_L");
        foreach(GameObject edge_l in Sup_Edges_L)
        {
            GoThroughBlocks(edge_l);
        }
    }

    void GoThroughBlocks(GameObject edge_l)
    {
        Physical_Materials.Add(edge_l.transform, Instantiate(floor, edge_l.transform.position, Quaternion.identity));
        Physical_Materials.Add(edge_l.transform, Instantiate(wall, edge_l.transform.position, Quaternion.identity));
    }
}