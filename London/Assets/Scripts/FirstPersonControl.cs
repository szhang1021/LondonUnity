using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;
    private float translation;
    private float straffe;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(straffe,0,translation);

        if(Input.GetKeyDown("escape"))
        	Cursor.lockState = CursorLockMode.None;
    }
}
