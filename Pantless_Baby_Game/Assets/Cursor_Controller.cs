using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Controller : MonoBehaviour
{

    public static Cursor_Controller instance;

    public Texture2D Passive_cursor, Target_cursor;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor_Controller.instance.activate_passive_cursor;
        activate_passive_cursor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activate_passive_cursor()
    {
        Cursor.SetCursor(Passive_cursor, new Vector2(Passive_cursor.width / 2, Passive_cursor.height / 2), CursorMode.Auto);
    }
}
