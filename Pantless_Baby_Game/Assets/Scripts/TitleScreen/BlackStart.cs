using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlackStart : MonoBehaviour
{
    Image rend;
    float blaAlpha = 0;

    void Start()
    {
        rend = GetComponent<Image>();
    }

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
