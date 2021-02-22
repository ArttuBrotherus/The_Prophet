using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperTools : MonoBehaviour
{

    delegate void devTools();

    public Text DToolTxt;

    // Start is called before the first frame update
    void Start()
    {
        DToolTxt.text = "";

        List<devTools> dTools = new List<devTools>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(DToolTxt.text == "")
            {
                DToolTxt.text = "Select:\n" +
                    "(T) Close menu\n" +
                    "(M) Trajectory markers";
            }
            else
            {
                DToolTxt.text = "";
            }
        }

        /////
       
        if(DToolTxt.text != "")
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log("In business");
            }
        }
    }
}
