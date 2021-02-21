using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{

	private Rigidbody rb;
    public float speed;
    public int jumpforce;
    public int gravityforce;
    private Vector3 jump;

	void Start(){
		rb = GetComponent<Rigidbody> ();
        jump = new Vector3(0,1,0);
        Physics.gravity = new Vector3(0,-gravityforce,0);
	}
    // Update is called once per frame
    void FixedUpdate()
    {
        bool moveUp = Input.GetKey("space");


        if (moveUp == true){
            rb.velocity = new Vector3(0,jumpforce,0);
        }
    }
}
