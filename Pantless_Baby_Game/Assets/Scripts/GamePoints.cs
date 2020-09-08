using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GamePoints : MonoBehaviour {

    public int points;

    public Text pointsText;

    void Update()
    {
        pointsText.text = "Points: " + points;
    }

}
