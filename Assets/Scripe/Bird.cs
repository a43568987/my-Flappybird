using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public int score = 0;
    public int power;
    private Rigidbody rig;
    public bool isGameOver = false;
    int mark = 0;

	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	public void OnUpdate () {
        if (InputManager.mark > mark)
        {
            fly();
            mark++;
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "wall")
        {
            gameover();
            Debug.Log("gg");
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "goal" && isGameOver == false)
        {
            score++;
            Debug.Log("++");
        }
        if (other.gameObject.tag == "MainCamera")
        {
            gameover();
        }
    }


    void gameover()
    {
        isGameOver = true;
        rig.AddForce(Vector3.down * power * 2 * rig.mass);
        GetComponent<Collider>().isTrigger = true;
    }

    void fly()
    {
        if (!isGameOver)
        {
            rig.velocity = Vector3.zero;
            rig.AddForce(Vector3.up * power * rig.mass);
        }
    }
}
