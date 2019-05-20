using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimension : MonoBehaviour {


    [SerializeField]
    Transform destination;
    [SerializeField]
    GameObject NeedToDestroy;
    [SerializeField]
    GameObject Gateup;
    int hp;

	// Use this for initialization
	void Start () {
        hp = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&&this.gameObject.tag == "Dimension")
        {
            other.GetComponent<Transform>().position = destination.position;
        }

        if (other.tag == "Axe" && this.gameObject.tag == "NeedToDestroy")
        {
            hp--;
            if (hp <= 0)
            {
                if(Gateup!=null)
                Gateup.GetComponent<Transform>().position = new Vector3(Gateup.GetComponent<Transform>().position.x, 
                    Gateup.GetComponent<Transform>().position.y+50, Gateup.GetComponent<Transform>().position.z);
                if(NeedToDestroy!=null)
                Destroy(NeedToDestroy);
                Destroy(gameObject);
            }
        }
    }
}
