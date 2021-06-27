using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCutS : MonoBehaviour
{

    //access via editor
    public GameObject DALText;

    float phaseTime;
    string phase = "start";
    string nextPhase = "blackFadesOut";

    SpriteRenderer bRend;
    float blackAlpha = 1f;

    // Start is called before the first frame update
    void Start()
    {
        bRend = GetComponent<SpriteRenderer>();
        phaseTime = 1f;
        StartCoroutine("waitNewPhase");
    }

    private IEnumerator waitNewPhase()
    {
        yield return new WaitForSeconds(phaseTime);
        phase = nextPhase;
        StopCoroutine("waitNewPhase");
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == "blackFadesOut")
        {
            fadeBlackOut();
        }else if(phase == "addText")
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
            phaseTime = 2.25f;
            nextPhase = "addText";
            StartCoroutine("waitNewPhase");
        }
        else
        {
            bRend.color = new Color(0f, 0f, 0f, blackAlpha);
        }
    }



    /*
     
    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(waitTime);
        roo.velocity = new Vector2(jumpVelX * this.transform.lossyScale.x, jumpVelY);
    }

    */
}
