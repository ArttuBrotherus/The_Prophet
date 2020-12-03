using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This file showcases a working coroutine in action

public class OneWayPlatformEffectorReversal : MonoBehaviour
{
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EffectorReversal()
    {
        gameObject.layer = 0;
        coroutine = revBackRotation();
        StartCoroutine(coroutine);
    }

    private IEnumerator revBackRotation()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.layer = 11;
    }
}
