﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start of the button");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startTheGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnMouseDown()
    {
        Debug.Log("button mouse down");
    }

}
