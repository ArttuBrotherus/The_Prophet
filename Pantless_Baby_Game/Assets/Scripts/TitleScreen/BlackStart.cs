using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlackStart : MonoBehaviour
{

    public GameObject selectionPointer;

    Image rend;
    float blaAlpha = 0;

    // Start is called before the first frame update
    void Start()
    {
        var beginControl = selectionPointer.GetComponent<StartMenuScript>();
        beginControl.enabled = false;
        rend = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        blaAlpha += Time.deltaTime * 0.5f;
        if(blaAlpha > 1f)
        {
            SceneManager.LoadScene("1stCutscene");
        }
        rend.color = new Color(0f, 0f, 0f, blaAlpha);
    }
}
