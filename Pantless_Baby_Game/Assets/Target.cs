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

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            InitiateOrbiting(1f);

        }else if (Input.GetMouseButtonDown(0))
        {
            InitiateOrbiting(-1f);
        }
    }

    void InitiateOrbiting(float orbiting_number)
    {
        GetComponent<SpriteRenderer>().sprite = sprite2;
        var player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        if (player_controller.IsMovementNormal())
        {
            //If left m. button pressed, value is -1, otherwise 1. -1 means orbiting takes
            //place counter-clockwise, 1 means clockwise

            player_controller.StartRotation(this.gameObject.transform.GetChild(0), orbiting_number);
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
