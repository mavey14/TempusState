using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour {

    [SerializeField]
    private GameObject YoungOne;
    [SerializeField]
    private GameObject OldOne;
    [SerializeField]
    private GameObject SkelExplode;
    [SerializeField]
    private Transform SkelExplodePos;
    Vector3 direct;
    CapsuleCollider skellcolider;
    //[SerializeField]
    //GameObject DeathExplode;
    Animator anim;
    enum EnemyState { Sleep,Battle,Death};
    EnemyState estate;
    float speed = 18f;
    Rigidbody rb;
    float rotSpeed = 3f;
    public bool canattack;
    public int noattack;
    int HP;
    bool awake;
    [SerializeField]
    GameObject gmscript;
    Vector3 origpos;
    [SerializeField]
    GameObject instruction;

    // Use this for initialization
    void Start()
    {
       
        estate = EnemyState.Sleep;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        canattack = true;
        direct = Vector3.zero;
        HP = 3;
        awake = false;
        origpos = transform.position;
        skellcolider = GetComponent<CapsuleCollider>();
        skellcolider.enabled = true;
        //Debug.Log(gameObject.name + " " + origpos);
        //foreach (Transform child in transform)
        //    child.gameObject.SetActive(false);
        if (gameObject.name == "Tutorial")
        {
            awake = true;
            anim.SetBool("Awake", awake);
            estate = EnemyState.Battle;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (awake == true)
        {
            anim.enabled = !gmscript.GetComponent<GMScript>().timestop;
            if (YoungOne.activeSelf == true)
            {
                direct = YoungOne.GetComponent<Transform>().transform.position - this.transform.position;
            }
            if (OldOne.activeSelf == true)
            {
                direct = OldOne.GetComponent<Transform>().transform.position - this.transform.position;
            }
            direct.y = 0;
            //Debug.Log(Vector3.Distance(direct, this.transform.position));
            float angle = Vector3.Angle(direct, this.transform.forward);

            switch (estate)
            {
                //case EnemyState.Sleep:
                //    AwakeEne(direct);
                //    break;
                case EnemyState.Battle:
                    if (gmscript.GetComponent<GMScript>().timestop == false)
                        Battle(direct, angle);
                    break;
                case EnemyState.Death:
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            Debug.Log("test");
            awake = true;
            
            anim.SetBool("Awake", awake);
            estate = EnemyState.Battle;
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            ReduceHP();
        }



    }

    //void AwakeEne(Vector3 direct)
    //{
    //    if (Vector3.Distance(direct,this.transform.position) < 10f)
    //    {
    //        anim.SetBool("Awake", true);
    //        estate = EnemyState.Battle;
    //    }
    //}


    void Battle(Vector3 direct, float angle)
    {
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation
            (direct), rotSpeed * Time.deltaTime);

        if (direct.magnitude > 10) //change gaano kalayo ang player bago umuatak
        {
            noattack = 0;
            rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
            anim.SetBool("Run", true);
            anim.SetBool("Idle", false);
            anim.SetInteger("Attack",noattack);
        }
        else 
        {
            if (canattack == true)
            {
                anim.SetBool("Idle", false);
                anim.SetBool("Run", false);
                int random = Random.Range(1, 4);
                noattack = random;
                anim.SetInteger("Attack", noattack);
                anim.SetBool("Idle", true);
                StartCoroutine(AttackCD());
                canattack = false;
            }  

        }
    }

    IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Idle",false);
        canattack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Old")
        {
            awake = true;
            FindObjectOfType<Audiomanager>().Play("SkeletonForm");
            anim.SetBool("Awake", awake);
            estate = EnemyState.Battle;
        }
    }

    public void ReduceHP()
    {
        HP--;
        if (HP == 0)
        {
           
            FindObjectOfType<Audiomanager>().Play("SkeletonDeath");
            GameObject vey = Instantiate(SkelExplode,SkelExplodePos.transform.position, Quaternion.identity);
            if (gameObject.name == "Tutorial")
            {
                Destroy(vey, 1f);
                if(instruction!=null)
                instruction.SetActive(true);
                Destroy(gameObject);
            }
            else
            {
                foreach (Transform child in transform)
                    child.gameObject.SetActive(false);
                skellcolider.enabled = false;
                Invoke("Revive", 10f);
                Destroy(vey, 1.5f);
            }
          
            //Destroy(this.gameObject);
        }
    }

    void Revive()
    {
        transform.position = origpos;
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
        skellcolider.enabled = true;
        resetall ();
    }

    public void resetall()
    {
        HP = 3;
        estate = EnemyState.Sleep;
        awake = false;
        noattack = 0;
        anim.SetBool("Awake",awake);
        anim.SetInteger("Attack", 0);
        anim.SetBool("Idle", false);
        anim.SetBool("Run", false);
        anim.Play("Sleep");
      
    }

}
