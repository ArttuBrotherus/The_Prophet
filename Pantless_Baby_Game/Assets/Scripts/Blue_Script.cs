using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Script : MonoBehaviour
{
    public float cycleTime;
    float progress = 0f;
    float dir = 1f;

    SpriteRenderer SRenderer;

    // Start is called before the first frame update
    void Start()
    {
        SRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        progress = progress + dir * Time.deltaTime;
        
        if(progress > 0.5f * cycleTime && progress < 0)
        {
            dir = dir == 1 ? -1 : 1;
        }
        else
        {
            SRenderer.color = new Color(1f, 110f, 153f, progress / (0.5f * cycleTime));
        }
    }
}
