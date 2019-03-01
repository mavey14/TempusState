using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    [SerializeField]
    GameObject obsenter;
    [SerializeField]
    GameObject obsexit;
    [SerializeField]
    GameObject Player;
    bool collide;
    // Use this for initialization
    void Start () {
        collide = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetKey(KeyCode.P))
        {
            collide = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("asda");
        if (collision.collider.tag == "Player" && collide == false)
        {
            Debug.Log("test");
            Vector3 dir = (Player.GetComponent<Transform>().localPosition- transform.position).normalized;
            if (Vector3.Dot(dir, transform.forward) > 0 && collide == false)
            {
                Debug.Log("Enter");
                collide = true;
                Debug.Log(collide);
                obsenter.SetActive(true);
                obsexit.SetActive(false);
            }

            if (collide==true)
            {
                
                Debug.Log("Exit");
                obsenter.SetActive(false);
                obsexit.SetActive(true);
                collide = false;

            }

        }
    }

    
}
