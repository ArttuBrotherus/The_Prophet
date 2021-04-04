using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Script : MonoBehaviour
{
    public float cycleTime;

    SpriteRenderer SRenderer;

    // Start is called before the first frame update
    void Start()
    {
        SRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var cycles = Time.time / cycleTime;
        var cycleFraction = cycles - Mathf.Floor(cycles);
        var colorFraction = cycleFraction < 0.5 ? cycleFraction : 1 - cycleFraction;

        SRenderer.color = new Color(0f, 0.43f, 0.6f, colorFraction * 2);
    }
}
