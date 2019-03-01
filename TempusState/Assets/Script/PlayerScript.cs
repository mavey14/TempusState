using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour {

    float zmov, speed;
    Vector3 velo,pastpos;
    Rigidbody rb;
    private bool move, turn;
    [HideInInspector]
    public Animator panim;
    [HideInInspector]
    public int noclicks;
    bool canclick;
    bool recordpos;
    [SerializeField]
    GameObject oldyoung;
    [SerializeField]
    GameObject gm;
    PlayerUIScript pui;
    float pasthp;

    

    // Use this for initialization
    void Start()
    {
       
        speed = 15f;
        move = turn = true;
        rb = GetComponent<Rigidbody>();
        panim = GetComponent<Animator>();
        pui = GetComponent<PlayerUIScript>();
        noclicks = 0;
        canclick = true;
        recordpos = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        move = Input.GetAxisRaw("Vertical") != 0 && noclicks == 0 && !panim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack")
            || Input.GetAxisRaw("Horizontal") != 0 && noclicks == 0 && !panim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack");

        GetMovement();

        if (move)
        {
            pmove(zmov);
        }
        if (turn && noclicks == 0 && !panim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"))
        {
            pturn();
        }

        Skill();
        if (!recordpos)
        {
            Invoke("get3secpast", 5f);
            recordpos = !recordpos;
        }
        AttackandDodge();

        if(this.gameObject.activeSelf)
        panim.SetBool("move", move);

        if (Input.GetKeyDown(KeyCode.M))
        {
            resetanim();
        }
        //Debug.Log(gm.GetComponent<GMScript>().cskill);
    }

    

    void get3secpast()
    {
        if(gm.GetComponent<GMScript>().cskill==0)
        pastpos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        pasthp = pui.hpts;
        recordpos = !recordpos;
    }


    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.GetComponent<GMScript>().cskill == 0&&pui.mnpts>0)
        {
            TransformtoOld();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && gm.GetComponent<GMScript>().cskill == 1)
        {
            Transformtochild();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && gm.GetComponent<GMScript>().cskill == 0 && pui.mnpts > 0)
        {
            gm.GetComponent<GMScript>().cskill = 2;
            gm.GetComponent<GMScript>().timestop = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && gm.GetComponent<GMScript>().cskill == 2)
        {
            gm.GetComponent<GMScript>().cskill = 0;
            gm.GetComponent<GMScript>().timestop = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && gm.GetComponent<GMScript>().cskill == 0&& pui.mnpts > 40)
        {
            if (pastpos != Vector3.zero)
            {
                pui.mnpts -= 40;
                pui.hpts = pasthp;
                transform.position = pastpos;
            }


        }
    }


    public void TransformtoOld()
    {
        gm.GetComponent<GMScript>().cskill = 1;
        noclicks = 0;
        oldyoung.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y-4.1f,
            transform.position.z);
        oldyoung.GetComponent<Transform>().transform.rotation = transform.rotation;
        oldyoung.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void Transformtochild()
    {
        gm.GetComponent<GMScript>().cskill = 0;
        noclicks = 0;
        oldyoung.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y+4f,
            transform.position.z);
        oldyoung.GetComponent<Transform>().transform.rotation = transform.rotation;
        oldyoung.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void resetanim()
    {
        canclick = true;
        noclicks = 0;
        panim.Play("Idle");
    }

    void ComboStarter()
    {
        if (canclick)
        {
            noclicks++;
        }

        if (noclicks == 1)
        {
            panim.SetInteger("lightattack", 1);
        }
    }

    public void Combo()
    {
        canclick = false;
        if (panim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack") && noclicks == 1)
        {
            panim.SetInteger("lightattack", 0);
            canclick = true;
            noclicks = 0;
        }
        else if (panim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack") && noclicks >= 2)
        {
            panim.SetInteger("lightattack", 2);
            canclick = true;
        }
        else if (panim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack2") && noclicks == 2)
        {
            panim.SetInteger("lightattack", 0);
            canclick = true;
            noclicks = 0;
        }
        else if (panim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack2") && noclicks >= 3)
        {
            panim.SetInteger("lightattack", 3);
            canclick = true;
        }
        else if (panim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack3"))
        {
            panim.SetInteger("lightattack", 0);
            canclick = true;
            noclicks = 0;
        }
        else
        {
            panim.SetInteger("lightattack", 0);
            canclick = true;
            noclicks = 0;
        }
    }


    void AttackandDodge()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ComboStarter();
        }
        if (Input.GetMouseButtonDown(1))
        {
            panim.SetTrigger("heavyattack");
        }

        if (Input.GetKeyDown(KeyCode.Space) && !panim.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
        {
            panim.SetTrigger("dodge");

        }
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
        velo = transform.TransformDirection(new Vector3(0, 0, zmov > 0 ? zmov : -zmov)).normalized * speed;
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

    void dead()
    {
        //StartCoroutine(PlayerDeath());
    }
}
