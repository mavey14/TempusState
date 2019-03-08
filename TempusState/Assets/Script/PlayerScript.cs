using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour {

    float zmov, speed;
    Vector3 velo;
    Rigidbody rb;
    private bool move, turn;
    [HideInInspector]
    public Animator panim;
    [HideInInspector]
    public int noclicks;
    bool canclick;
    public bool backtrack;
    [SerializeField]
    GameObject old;
    [SerializeField]
    GameObject gm;
    PlayerUIScript pui;
    [SerializeField]
    GameObject[] SkillEffects;
    [SerializeField]
    Transform[] Effects;
    

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
        backtrack = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        move = Input.GetAxisRaw("Vertical") != 0 && noclicks == 0 && !panim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack")
            || Input.GetAxisRaw("Horizontal") != 0 && noclicks == 0 && !panim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack");

        speed = panim.GetCurrentAnimatorStateInfo(0).IsName("Dodge") == true ? 28f : 15f;

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

        if(this.gameObject.activeSelf)
        panim.SetBool("move", move);

        if (Input.GetKeyDown(KeyCode.F))
        {
            resetanim();
        }
        //Debug.Log(gm.GetComponent<GMScript>().cskill);
    }

   


    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.GetComponent<GMScript>().cskill == 0&&pui.mnpts>0)
        {
            
            StartCoroutine(TransformtoOld());

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && gm.GetComponent<GMScript>().cskill == 0 && pui.mnpts > 0)
        {
            StartCoroutine(Freeze());

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && gm.GetComponent<GMScript>().cskill == 2)
        {
            gm.GetComponent<GMScript>().cskill = 0;
            gm.GetComponent<GMScript>().timestop = false;
            SkillEffects[01].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && gm.GetComponent<GMScript>().cskill == 0&& pui.mnpts >0)
        {
            backtrack = true;
            gm.GetComponent<GMScript>().cskill = 3;
            StartCoroutine(BackTrack());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && gm.GetComponent<GMScript>().cskill == 3)
        {
            gm.GetComponent<GMScript>().cskill = 0;
            backtrack = false;
            SkillEffects[02].SetActive(false);
        }
    }

    public IEnumerator BackTrack()
    {
        SkillEffects[02].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SkillEffects[02].SetActive(false);
    }

    public void turnoffeffects3()
    {
        SkillEffects[02].SetActive(false);
    }

    public void turnoffeffects()
    {
        SkillEffects[01].SetActive(false);
    }

    IEnumerator Freeze()
    {
        panim.SetTrigger("freeze");
        yield return new WaitForSeconds(1f);
        SkillEffects[01].SetActive(true);
        gm.GetComponent<GMScript>().cskill = 2;
        gm.GetComponent<GMScript>().timestop = true;
    }

    IEnumerator TransformtoOld()
    {
        panim.SetTrigger("age");
        yield return new WaitForSeconds(1f);
        Instantiate(SkillEffects[0],Effects[0].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        gm.GetComponent<GMScript>().cskill = 1;
        noclicks = 0;
        old.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y-4.1f,
            transform.position.z);
        old.GetComponent<Transform>().transform.rotation = transform.rotation;
        old.SetActive(true);
        this.gameObject.SetActive(false);
    }

    //public void Transformtochild()
    //{
    //    //old.SetActive(true);
    //    //resetanim();
    //    ////yield return new WaitForSeconds(1f);
    //    //gm.GetComponent<GMScript>().cskill = 0;
    //    //noclicks = 0;
    //    //old.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y+4f,
    //    //    transform.position.z);
    //    //old.GetComponent<Transform>().transform.rotation = transform.rotation;
    //    //this.gameObject.SetActive(false);
    //    Debug.Log("test");
    //}

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
        else if (panim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack2") && noclicks >= 3&&this.gameObject.name=="YoungOne")
        {
            panim.SetInteger("lightattack", 3);
            canclick = true;
        }
        else if (panim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack3") && this.gameObject.name == "YoungOne")
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
        if (Input.GetMouseButtonDown(1)&& this.gameObject.name == "YoungOne")
        {
            panim.SetTrigger("heavyattack");
        }

        if (Input.GetKeyDown(KeyCode.Space) && !panim.GetCurrentAnimatorStateInfo(0).IsName("Dodge")&&this.gameObject.name=="YoungOne"&&pui.stam>=50)
        {
            pui.DodgeStamina();
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
