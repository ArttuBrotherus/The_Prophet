using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuScript: MonoBehaviour
{

    RectTransform recTra;

    int menuOpti = 2;

    public GameObject blackScreen;
    BlackStart blackSpt;

    //menu-texts:
    //public GameObject mTexts;

    // Start is called before the first frame update
    void Start()
    {
        recTra = this.GetComponent<RectTransform>();
        blackSpt = blackScreen.GetComponent<BlackStart>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveThroughNo(1);
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            moveThroughNo(- 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            chooseFunc();
        }
    }

    void chooseFunc()
    {
        if (menuOpti == 2)
        {
            Debug.Log("Continue");
        }
        else if (menuOpti == 1)
        {
            blackSpt.enabled = true;
            this.enabled = false;
        }
        else
        {
            Application.Quit();
        }
    }

            void moveThroughNo(int nput)
    {
        menuOpti += nput;
        if(menuOpti > 2)
        {
            recTra.anchoredPosition = new Vector2(recTra.anchoredPosition.x, - 149.755f);
            menuOpti = 0;
        }else if(menuOpti < 0)
        {
            recTra.anchoredPosition = new Vector2(recTra.anchoredPosition.x, -29.755f);
            menuOpti = 2;
        }
        else
        {
            recTra.anchoredPosition = new Vector2
                (recTra.anchoredPosition.x, recTra.anchoredPosition.y + 60 * nput);
        }
    }
}