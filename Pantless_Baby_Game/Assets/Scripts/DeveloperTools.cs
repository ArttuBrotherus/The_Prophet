using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DeveloperTools : MonoBehaviour
{
    private IEnumerator coroutine;

    delegate void devTools();

    public Text DToolTxt;
    public GameObject markerParticle;
    public GameObject player;

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
                coroutine = markerAddition();
                StartCoroutine(coroutine);
            }
        }
    }

    private IEnumerator markerAddition()
    {
        while (true)
        {
            Instantiate(markerParticle, player.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(0.15f);
        }
    }
}
