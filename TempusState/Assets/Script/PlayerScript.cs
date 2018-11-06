using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour {

    float zmov, speed;
    Vector3 velo,pastpos;
    Rigidbody rb;
    private bool move, turn;
    Animator panim;
    public int noclicks;
    bool canclick;
    bool recordpos;
    [SerializeField]
    GameObject oldyoung;
    [SerializeField]
    GameObject gm;
    [SerializeField]
    GameObject cam;
    

    // Use this for initialization
    void Start()
    {
        speed = 15f;
        move = turn = true;
        rb = GetComponent<Rigidbody>();
        panim = GetComponent<Animator>();
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


    }

    

    void get3secpast()
    {
        //Debug.Log("record");
        if(gm.GetComponent<GMScript>().cskill==0)
        pastpos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        //Debug.Log("record "+ pastpos);
        //gethealth
        recordpos = !recordpos;
    }


    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.GetComponent<GMScript>().cskill == 0)
        {
            gm.GetComponent<GMScript>().cskill = 1;
            noclicks = 0;
            oldyoung.GetComponent<Transform>().position = new Vector3(transform.position.x,transform.position.y,
                transform.position.z);
            oldyoung.GetComponent<Transform>().transform.rotation = transform.rotation;
            oldyoung.SetActive(true);
            this.gameObject.SetActive(false);

            Debug.Log("Transform");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && gm.GetComponent<GMScript>().cskill == 1)
        {
            gm.GetComponent<GMScript>().cskill = 0;
            noclicks = 0;
            oldyoung.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y-5f,
                transform.position.z);
            oldyoung.GetComponent<Transform>().transform.rotation = transform.rotation;
            oldyoung.SetActive(true);
            this.gameObject.SetActive(false);

            Debug.Log("Pampabata");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && gm.GetComponent<GMScript>().cskill == 0)
        {
            
            gm.GetComponent<GMScript>().cskill = 2;
            gm.GetComponent<GMScript>().timestop = true;
            Debug.Log("StopTime");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && gm.GetComponent<GMScript>().cskill == 2)
        {

            gm.GetComponent<GMScript>().cskill = 0;
            gm.GetComponent<GMScript>().timestop = true;
            Debug.Log("Deactive stoptime");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && gm.GetComponent<GMScript>().cskill == 0)
        {
            if (pastpos != Vector3.zero)
            {
                transform.position = pastpos;
            }

            Debug.Log("Return past");

        }
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
