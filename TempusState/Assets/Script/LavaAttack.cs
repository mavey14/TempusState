using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaAttack : MonoBehaviour {

    
    public Vector3 player;
    float speed;
    Rigidbody rb;
    public GameObject[] waypoints;
    int currentWP = 0;
    float accuracyWP = 10f;
    float rotSpeed = 2f;
    // Use this for initialization
    void Start () {
        speed = 50f;
        rb = GetComponent<Rigidbody>();
        if (this.gameObject.tag == "FB")
            Destroy(this.gameObject, 1f);
        else if (this.gameObject.tag == "meteor")
            Destroy(this.gameObject, 5f);
        else if (this.gameObject.tag == "GA")
            Destroy(this.gameObject,1f);
        else if (this.gameObject.tag == "explo")
            Destroy(this.gameObject, 1f);
    }
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.tag == "Meteor")
            transform.Translate(0, -20f * Time.deltaTime, 0);
        else if (this.gameObject.tag == "FB")
            transform.position = Vector3.MoveTowards(transform.position, player, speed * Time.deltaTime);
        else if (this.gameObject.tag == "tr")
        {
            Patrol();
        }



        //transform.position = Transform.(transform.position, player, speed * Time.deltaTime);
        //Vector3 velo = transform.TransformDirection(new Vector3(0, 0,-1)).normalized * speed;
        //rb.MovePosition(transform.position + velo * Time.fixedDeltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, player, speed * Time.deltaTime);
        //transform.position += Vector3.forward*speed * Time.deltaTime;

    }


    void Patrol()
    {
        Vector3 dir;
        if (waypoints.Length > 0)
        {
            if (Vector3.Distance(waypoints[currentWP].transform.position,
                transform.position) < accuracyWP)
            {
                currentWP++;
                if (currentWP >= waypoints.Length)
                {
                    currentWP = 0;
                }

            }

            dir = waypoints[currentWP].transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);
            transform.Translate(0, 0, 10 * Time.deltaTime);
           // rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerUIScript>().Damage(5);
        }
    }
}
