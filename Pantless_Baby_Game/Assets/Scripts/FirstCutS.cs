using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class FirstCutS : MonoBehaviour
{

    //access via editor
    public GameObject DALText;

    SpriteRenderer bRend;
    float blackAlpha = 1f;

    Tuple<float, string>[] phases =
        {
        new Tuple<float, string> (1f, "start"),
        new Tuple<float, string> (3f, "blackFadesOut"),
        new Tuple<float, string> (1.5f, "addText"),
        new Tuple<float, string> (3.5f, "level1"),
        };
    int phaseIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        bRend = GetComponent<SpriteRenderer>();
        StartCoroutine(executePhase());
    }

    private IEnumerator executePhase()
    {
        while(true) { 
            yield return new WaitForSeconds(currentPhase.Item1);
            switch (currentPhase.Item2)
            {
                case "addText":
                    DALText.SetActive(true);
                    break;
                case "level1":
                    SceneManager.LoadScene("Level-1");
                    break;
            }
            if (phaseIndex < phases.Length - 1)
            {
                phaseIndex++;
            } else
            {
                break;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(currentPhase.Item2 == "blackFadesOut")
        {
            blackAlpha -= Time.deltaTime * 0.5f;
            bRend.color = new Color(0,0,0, blackAlpha < 0f ? 0: blackAlpha);
        }
    }

    Tuple<float, string> currentPhase
    {
        get 
        { 
            return phases[phaseIndex]; 
        }
    }
}
