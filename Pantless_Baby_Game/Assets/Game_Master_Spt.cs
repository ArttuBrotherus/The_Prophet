using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Game_Master_Spt : MonoBehaviour {

    public int points;

    public Text pointsText;

    void Update()
    {
        pointsText.text = "Points: " + points;
    }

}
