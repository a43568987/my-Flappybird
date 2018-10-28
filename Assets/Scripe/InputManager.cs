using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager{

    public static int mark = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void OnUpdate () {
        if (Input.GetMouseButtonDown(0))
        {
            mark++;
        }
        
    }
}
