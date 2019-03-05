using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldOneScript : MonoBehaviour {


    float zmov, speed;
    Vector3 velo;
    Rigidbody rb;
    private bool move, turn;
    [HideInInspector]
    public Animator panim;
    [HideInInspector]
    public int noclicks;
    bool canclick;
    [SerializeField]
    GameObject young;
    PlayerUIScript pui;
    [SerializeField]
    GameObject gm;
    [SerializeField]
    GameObject[] SkillEffects;
    [SerializeField]
    Transform[] Effects;
    // Use this for initialization
    void Start () {
        speed = 15f;
        move = turn = true;
        rb = GetComponent<Rigidbody>();
        panim = GetComponent<Animator>();
        pui = GetComponent<PlayerUIScript>();
        noclicks = 0;
        canclick = true;
    }
	
	// Update is called once per frame
	void Update () {

        move = Input.GetAxisRaw("Vertical") != 0 && noclicks == 0 
            || Input.GetAxisRaw("Horizontal") != 0 && noclicks == 0 ;

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
        AttackandDodge();

        if (this.gameObject.activeSelf)
            panim.SetBool("move", move);

        if (Input.GetKeyDown(KeyCode.F))
        {
            resetanim();
        }
    }

    void Skill()
    {
         if (Input.GetKeyDown(KeyCode.Alpha1) && gm.GetComponent<GMScript>().cskill == 1)
        {
            Transformtochild();

        }
       
    }

    public void Transformtochild()
    {
        resetanim();
        //yield return new WaitForSeconds(1f);
        gm.GetComponent<GMScript>().cskill = 0;
        noclicks = 0;
        young.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y + 4f,
            transform.position.z);
        young.GetComponent<Transform>().transform.rotation = transform.rotation;
        young.SetActive(true);
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
}
