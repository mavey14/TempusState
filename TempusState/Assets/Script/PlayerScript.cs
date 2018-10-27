using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    float zmov,speed;
    Vector3 velo;
    Rigidbody rb;
    private bool move, turn;
    Animator panim;

	// Use this for initialization
	void Start () {
        speed = 10f;
        move = turn = true;
        rb = GetComponent<Rigidbody>();
        panim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        move = Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0;

        GetMovement();

        if (move)
        {
            pmove(zmov);
        }
        if (turn)
        {
            pturn();
        }

        panim.SetBool("move", move);
        
	}

    void pturn()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, Camera.main.transform.eulerAngles.y - 45, 0), 0.1f);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 45, 0), 0.1f);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 205, 0), 0.1f);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, Camera.main.transform.eulerAngles.y - 205, 0), 0.1f);
        }
        else if (Input.GetKey(KeyCode.W))
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0), 0.1f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, Camera.main.transform.eulerAngles.y - 180, 0), 0.1f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, Camera.main.transform.eulerAngles.y - 90, 0), 0.1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 90, 0), 0.1f);
        }
    }

    void pmove(float zmov)
    {
        velo = transform.TransformDirection(new Vector3(0, 0, zmov > 0 ? zmov:-zmov)).normalized * speed;
        rb.MovePosition(transform.position + velo * Time.fixedDeltaTime);
    }

    void GetMovement()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            zmov = Input.GetAxis("Vertical");
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            zmov = Input.GetAxis("Horizontal");
        }
    }
}
