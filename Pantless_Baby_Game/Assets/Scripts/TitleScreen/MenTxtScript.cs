using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenTxtScript : MonoBehaviour
{

    //This script handles a lot of the 'haxxed' animations
    //for the title menu upon start-up

    public GameObject hoodIndicator;
    RectTransform indica;
    StartControl staCon;

    Image img;
    int phase = 0;
    //below: used for the img alpha value
    float alp = 0;

    Vector2 destination = new Vector2(-50f, -29.755f);

    public GameObject mTexts;

    public GameObject gramophone;
    AudioPlayback sndPlayer;
    public AudioClip rHearts;

    // Start is called before the first frame update
    void Start()
    {
        indica = hoodIndicator.GetComponent<RectTransform>();
        staCon = hoodIndicator.GetComponent<StartControl>();
        img = hoodIndicator.GetComponent<Image>();
        sndPlayer = gramophone.GetComponent<AudioPlayback>();

        staCon.enabled = false;
        img.color = new Color(1f, 1f, 1f, 0f);

        StartCoroutine("beforeFadeIn");
    }

    private IEnumerator beforeFadeIn()
    {
        yield return new WaitForSeconds(1f);

        sndPlayer.oneSound(rHearts, 0.75f);

        phase = 1;
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        //0: The fade-in hasn't started yet
        //1: The fade-in is in progress
        if (phase == 1)
        {
            increaseAlpha();

            //2: Wait a bit before the movement is started
        } else if (phase == 2)
        {
            moveTowards();
        }
    }

    void increaseAlpha()
    {
        alp += Time.deltaTime;
        if (alp > 1f)
        {
            img.color = new Color(1f, 1f, 1f, 1f);
            phase = 0;
            StartCoroutine("beforeMovement");
        }
        img.color = new Color(1f, 1f, 1f, alp);
    }

    private IEnumerator beforeMovement()
    {
        yield return new WaitForSeconds(1f);
        phase = 2;
        yield break;
    }

    void moveTowards()
    {
        var x = indica.anchoredPosition.x - Time.deltaTime * 45f;

        if(indica.anchoredPosition.x < destination.x)
        {
            indica.anchoredPosition = destination;
            phase = 0;
            normalMenu();
        }

        var progress = indica.anchoredPosition.x / destination.x;
        var y = progress * destination.y;

        indica.anchoredPosition = new Vector2(x, y);
    }

    void normalMenu()
    {
        staCon.enabled = true;
        mTexts.SetActive(true);
    }
}
