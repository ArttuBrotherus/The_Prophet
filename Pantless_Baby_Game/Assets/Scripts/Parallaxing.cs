using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;			// Array (list) of all the back- and foregrounds to be parallaxed
	public float[] parallaxScales;			// The proportion of the camera's movement to move the backgrounds by
    private Vector3[] backgroundStartPos;

	private Transform cam;					// reference to the main cameras transform
	private Vector3 initialCamPos;			// the position of the camera in the previous frame

	// Is called before Start(). Great for references.
	void Awake () {
		// set up camera the reference
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		// The previous frame had the current frame's camera position
		initialCamPos = cam.position;
        backgroundStartPos = backgrounds.Select(bg => bg.localPosition).ToArray();
    }
	
	// Update is called once per frame
	void Update () {
        // for each background
        var cameraMoved = cam.position - initialCamPos;
        if(cameraMoved.y < 0)
        {
            cameraMoved.y = 0;
        }
        for (int i = 0; i < backgrounds.Length; i++) {
            backgrounds[i].localPosition = backgroundStartPos[i] - cameraMoved * parallaxScales[i];
		}
	}
}
