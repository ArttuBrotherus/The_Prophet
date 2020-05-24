using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Target : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        GetComponent<SpriteRenderer>().sprite = sprite2;
        Debug.Log("Ready");
    }
}
