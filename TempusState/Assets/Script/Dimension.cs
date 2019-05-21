using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimension : MonoBehaviour {


    [SerializeField]
    Transform destination;
    [SerializeField]
    GameObject[] NeedToDestroy;
    [SerializeField]
    GameObject[] Gateup;
    int hp;
    public static bool isbattle;
    [SerializeField]
    GameObject bossneedtoawake;
    [SerializeField]
    GameObject gmscript;

	// Use this for initialization
	void Start () {
        hp = 5;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && this.gameObject.tag == "Dimension" && isbattle == false)
        {
            other.GetComponent<Transform>().position = destination.position;
        }

        if (other.tag == "Axe" && this.gameObject.tag == "NeedToDestroy")
        {
            hp--;
            if (hp <= 0)
            {
                foreach (var g in Gateup)
                {
                    if (g != null)
                    {
                        g.GetComponent<Transform>().position = new Vector3(g.GetComponent<Transform>().position.x,
                    g.GetComponent<Transform>().position.y + 50, g.GetComponent<Transform>().position.z);
                    }
                }

                foreach (var n in NeedToDestroy)
                {
                    if (n != null)
                    {
                        Destroy(n);
                    }
                }
                Destroy(gameObject);
            }
        }

        if (other.tag == "Player" && this.gameObject.tag == "Awaken")
        {
            bossneedtoawake.GetComponent<RuinBoss>().awke();
            isbattle = true;
        }

        if (other.tag == "Player" && this.gameObject.tag == "PortalToFloat")
        {
            if (gmscript != null)
            {
                gmscript.GetComponent<GMScript>().LoadLevel(0);
            }
        }


    }
}
