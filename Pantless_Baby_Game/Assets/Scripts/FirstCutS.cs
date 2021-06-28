using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCutS : MonoBehaviour
{

    //access via editor
    public GameObject DALText;

    string phase = "start";

    SpriteRenderer bRend;
    float blackAlpha = 1f;

    // Start is called before the first frame update
    void Start()
    {
        bRend = GetComponent<SpriteRenderer>();
        StartCoroutine(waitNewPhase(1f, "blackFadesOut"));
    }

    private IEnumerator waitNewPhase(float duration, string nextPhase)
    {
        yield return new WaitForSeconds(duration);
        phase = nextPhase;
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == "blackFadesOut")
        {
            fadeBlackOut();
        } else if(phase == "addText")
        {
            DALText.SetActive(true);
            phase = "";
        }
    }

    void fadeBlackOut()
    {
        blackAlpha -= Time.deltaTime * 0.5f;
        if(blackAlpha < 0f)
        {
            bRend.color = new Color(0f, 0f, 0f, 0f);
            phase = "";
            StartCoroutine(waitNewPhase(2.25f, "addText"));
        }
        else
        {
            bRend.color = new Color(0f, 0f, 0f, blackAlpha);
        }
    }

}
