using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit01 : MonoBehaviour
{

    private bool readyExit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (readyExit)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    private void OnTriggerEnter2D()
    {
        Debug.Log("J'arrive");
        readyExit = true;
    }

    private void OnTriggerExit2D()
    {
        readyExit = false;
    }
}
