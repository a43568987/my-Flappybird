using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public float speed = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void OnUpdate () {
        transform.Translate(Vector3.left * speed);
        if(transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
	}
}
