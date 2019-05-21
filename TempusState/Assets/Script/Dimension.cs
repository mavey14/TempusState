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
    [SerializeField]
    GameObject[] GateDown;
    int hp;
    public static bool isbattle;
    [SerializeField]
    GameObject bossneedtoawake;
    [SerializeField]
    GameObject gmscript;
    [SerializeField]
    GameObject[] UIactivate;
    static int activeCrystal;
    bool deactivate;

	// Use this for initialization
	void Start () {
        hp = 5;
        deactivate = true;

	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.M))
        {
            activeCrystal = 4;

        }

        if (activeCrystal == 4&&this.gameObject.name == "Awke")
        {
            GateDown[0].GetComponent<Transform>().position = new Vector3(GateDown[0].GetComponent<Transform>().position.x,
                    GateDown[0].GetComponent<Transform>().position.y + 50, GateDown[0].GetComponent<Transform>().position.z);
            activeCrystal = 0;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && this.gameObject.tag == "Dimension" && isbattle == false)
        {
            other.GetComponent<Transform>().position = destination.position;
        }

        if (other.tag == "Axe" && this.gameObject.tag == "NeedToDestroy"|| other.tag == "AxeOld" && this.gameObject.tag == "NeedToDestroy")
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
                FindObjectOfType<Audiomanager>().Play("EnemyHit");

                Destroy(gameObject);
            }
        }

        if (other.tag == "Player" && this.gameObject.tag == "Awaken")
        {
            if (bossneedtoawake != null)
            {
                bossneedtoawake.GetComponent<RuinBoss>().awke();
                foreach (var v in UIactivate)
                {
                    if(v!=null)
                    v.SetActive(true);
                }
                if (bossneedtoawake.GetComponent<RuinBoss>().tag == "RuinsBoss")
                {
                    GateDown[0].GetComponent<Transform>().position = new Vector3(GateDown[0].GetComponent<Transform>().position.x,
                        150f, GateDown[0].GetComponent<Transform>().position.z);
                }
                isbattle = true;
            }
            
          
        }

        if (other.tag == "Player" && this.gameObject.tag == "PortalToFloat")
        {
            if (gmscript != null)
            {
                gmscript.GetComponent<GMScript>().LoadLevel(0);
            }
        }


        if (other.tag == "Axe" && this.gameObject.tag == "Cactivate"&&deactivate==true|| other.tag == "AxeOld" && this.gameObject.tag == "Cactivate" && deactivate == true)
        {
            activeCrystal += 1;
            Material mymat = GetComponent<Renderer>().material;
            mymat.SetColor("_EmissionColor", Color.red);
            FindObjectOfType<Audiomanager>().Play("EnemyHit2");

            deactivate = false;
        }


    }
}
