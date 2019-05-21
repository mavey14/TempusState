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
    bool Delay;
    bool awakes;
    [SerializeField]
    GameObject portaltofloat;

    // Use this for initialization
    void Start () {
        astate = Armstate.idle;

        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        canattack = true;

        speed = 20f;
        rotSpeed = 3f;
        direct = Vector3.zero;
        Delay = false;
        awakes = false;
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
        if (Input.GetKeyDown(KeyCode.G)&&this.gameObject.tag=="RuinsBoss")
        {
            Debug.Log("test");
            StartCoroutine(Awakes());
            // anim.SetBool("Idle", false);
            //anim.SetBool("Move", true);
        }
        else if(Input.GetKeyDown(KeyCode.H) && this.gameObject.tag == "2Hand")
        {
            Debug.Log("test");
            astate = Armstate.battle;
            // anim.SetBool("Idle", false);
            //anim.SetBool("Move", true);
        }
        else if (Input.GetKeyDown(KeyCode.J) && this.gameObject.tag == "1Hand")
        {
            Debug.Log("test");
            astate = Armstate.battle;
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

        if (direct.magnitude > 20&&gameObject.tag == "1Hand") //change gaano kalayo ang player bago umuatak
        {
            noattack = 0;
            rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
            anim.SetBool("Move", true);
            anim.SetBool("Idle", false);
            anim.SetInteger("Attack", noattack);
        }
        else if (direct.magnitude > 25 && gameObject.tag == "2Hand") //change gaano kalayo ang player bago umuatak
        {
            noattack = 0;
            rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
            anim.SetBool("Move", true);
            anim.SetBool("Idle", false);
            anim.SetInteger("Attack", noattack);
        }
        else if (direct.magnitude > 40 && gameObject.tag == "RuinsBoss") //change gaano kalayo ang player bago umuatak
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
                Debug.Log("why random");
                noattack = Random.Range(1,4);
                anim.SetBool("Move", false);
                //if(this.gameObject.tag=="la")
                anim.SetInteger("Attack",noattack);
                //FindObjectOfType<Audiomanager>().Play("RuinSlam");
                StartCoroutine(AttackCD());
                canattack = false;
            }

        }
    }


    IEnumerator Awakes()
    {
        if (gameObject.tag == "RuinsBoss")
        {
            Debug.Log("test");
            awakes = true;
            anim.SetBool("Awake", awakes);
            yield return new WaitForSeconds(2f);
            anim.SetBool("Awake", false);
        }
        astate = Armstate.battle;
    }

    IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Idle", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Idle", false);
        canattack = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (noattack > 0&&!YoungOne.GetComponent<PlayerScript>().panim.GetCurrentAnimatorStateInfo(0).IsName("Dodge")&&Delay==false)
            {
                other.GetComponent<PlayerUIScript>().Damage(2);
                Delay = true;
                StartCoroutine(IDIOT());
                other.GetComponent<PlayerScript>().kb();
                Debug.Log("Damage Player");
            }
    
           
        }
    }


    IEnumerator IDIOT()
    {
        yield return new WaitForSeconds(3f);
        Delay = false;
    }

    public void awke()
    {
        StartCoroutine(Awakes());
    }

    public void Death()
    {
        anim.SetTrigger("Death");
        astate = Armstate.death;
        portaltofloat.SetActive(true);
    }
}
