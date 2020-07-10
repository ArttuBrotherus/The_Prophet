using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    GameObject[] Sup_Edges_L;
    public GameObject floor;
    public GameObject wall;

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
        var floorChild = Instantiate(floor);
        var wallChild = Instantiate(wall);
        floorChild.transform.parent = edge_l.transform;
        wallChild.transform.parent = edge_l.transform;

        //floorChild.transform. = 0.95f;
    }
}