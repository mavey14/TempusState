using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinBoss : MonoBehaviour {


    [SerializeField]
    private GameObject YoungOne;
    [SerializeField]
    private GameObject OldOne;
    Vector3 direct;
    [SerializeField]
    Animator anim;
    enum Armstate { idle, battle, death };
    Armstate astate;
    float speed;
    Rigidbody rb;
    float rotSpeed;
    public bool canattack;
    public int noattack;

    // Use this for initialization
    void Start () {
        astate = Armstate.idle;

        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        canattack = true;
        speed = 20f;
        rotSpeed = 3f;
        direct = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        if (YoungOne.activeSelf == true)
        {
            direct = YoungOne.GetComponent<Transform>().transform.position - this.transform.position;
        }
        if (OldOne.activeSelf == true)
        {
            direct = OldOne.GetComponent<Transform>().transform.position - this.transform.position;
        }
        direct.y = 0;
        if (Input.GetKeyDown(KeyCode.G)&&this.gameObject.tag=="la")
        {
            Debug.Log("test");
            astate = Armstate.battle;
            // anim.SetBool("Idle", false);
            //anim.SetBool("Move", true);
        }
        else if(Input.GetKeyDown(KeyCode.H) && this.gameObject.tag == "lr")
        {
            Debug.Log("test");
            astate = Armstate.battle;
            // anim.SetBool("Idle", false);
            //anim.SetBool("Move", true);
        }
        else if (Input.GetKeyDown(KeyCode.J) && this.gameObject.tag == "wb")
        {
            Debug.Log("test");
            astate = Armstate.battle;
            // anim.SetBool("Idle", false);
            //anim.SetBool("Move", true);
        }

        if (Input.GetKeyDown(KeyCode.V) && this.gameObject.tag == "la")
        {
            Destroy(this.gameObject);
            // anim.SetBool("Idle", false);
            //anim.SetBool("Move", true);
        }
        else if (Input.GetKeyDown(KeyCode.B) && this.gameObject.tag == "lr")
        {
            Destroy(this.gameObject);
            // anim.SetBool("Idle", false);
            //anim.SetBool("Move", true);
        }
        else if (Input.GetKeyDown(KeyCode.N) && this.gameObject.tag == "wb")
        {
            Destroy(this.gameObject);
            // anim.SetBool("Idle", false);
            //anim.SetBool("Move", true);
        }


        switch (astate)
        {
            case Armstate.idle:
                Idle();
                break;
            case Armstate.battle:
                Battle(direct);
                break;
            case Armstate.death:
                break;

        }
    }

    void Idle()
    {
        anim.SetBool("Idle", true);
    }

    void Battle(Vector3 direct)
    {
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation
            (direct), rotSpeed * Time.deltaTime);

        if (direct.magnitude > 32) //change gaano kalayo ang player bago umuatak
        {
            noattack = 0;
            rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
            anim.SetBool("Move", true);
            anim.SetBool("Idle", false);
            anim.SetInteger("Attack", noattack);
        }
        else
        {
            if (canattack == true)
            {
                if (this.gameObject.tag == "la" || this.gameObject.tag == "lr")
                {
                    noattack = Random.Range(1, 4);
                }
                else
                {
                    noattack = Random.Range(1, 5);
                }
                anim.SetBool("Move", false);
                //if(this.gameObject.tag=="la")
                anim.SetInteger("Attack",noattack);
                StartCoroutine(AttackCD());
                canattack = false;
            }

        }
    }



    IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Idle", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Idle", false);
        canattack = true;
    }
}
