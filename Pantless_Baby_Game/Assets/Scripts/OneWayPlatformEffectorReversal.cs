using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformEffectorReversal : MonoBehaviour
{
    private PlatformEffector2D effector;

    // Start is called before the first frame update
    void Start()
    {
        BPCoroutine = BackToNormalRotation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EffectorReversal()
    {
        effector.rotationalOffset = 180f;
        //StartCoroutine(BPCo);
    }

    /*
    IEnumerator BackToNormalRotation()
    {

    }
    */
}
