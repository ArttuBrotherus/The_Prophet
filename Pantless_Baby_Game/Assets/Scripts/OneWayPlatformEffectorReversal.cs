using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This file showcases a working coroutine in action

public class OneWayPlatformEffectorReversal : MonoBehaviour
{
    private PlatformEffector2D effector;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EffectorReversal()
    {
        effector.rotationalOffset = 180f;
        coroutine = revBackRotation();
        StartCoroutine(coroutine);
    }

    private IEnumerator revBackRotation()
    {
        yield return new WaitForSeconds(0.5f);
        effector.rotationalOffset = 0.0f;
    }
}
