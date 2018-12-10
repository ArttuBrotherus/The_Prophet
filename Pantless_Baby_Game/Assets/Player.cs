using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private Game_Master_Spt gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master_Spt>();
    }

    void Update () {
		//_
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Currency"))
        {
            Destroy(collision.gameObject);
            gm.points += 1;
        }
    }

}
