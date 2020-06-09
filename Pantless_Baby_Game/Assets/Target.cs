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
        GetComponent<SpriteRenderer>().sprite = sprite2;
        var player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        if (player_controller.IsMovementNormal())
        {
            player_controller.StartRotation(this.gameObject.transform.GetChild(0));
        }
    }

    private void OnMouseEnter()
    {
        Cursor_Controller.instance.activate_active_cursor();
    }

    private void OnMouseExit()
    {
        Cursor_Controller.instance.activate_passive_cursor();
    }
}
