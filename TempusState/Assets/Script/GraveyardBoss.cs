using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardBoss : MonoBehaviour {

    [SerializeField]
    private GameObject YoungOne;
    [SerializeField]
    private GameObject OldOne;
    [HideInInspector]
    public Animator anim;
    [SerializeField]
    GameObject[] Effects;
    Vector3 direction;
    public GameObject[] waypoints;
    int currentWP = 0;
    public float rotSpeed = 1f;
    float speed = 8f;
    float accuracyWP = 20f;
    bool canattack,heal, summonminions, spit;
    public int Attack;
    AnimatorStateInfo charPlayerStateInfo;
    [SerializeField]
    GameObject gmscript;
    Rigidbody rb;
    [SerializeField]
    GameObject[] skilleffects;
    [SerializeField]
    Transform skillpos;
    [SerializeField]
    GameObject Minios;
    BossUIScript buiscript;
    public enum BossState{Patrol,Battlemode,Death};
    public enum PhaseState { phase1, phase2, phase3 };

    public BossState bstate;
    public PhaseState pstate;

    // Use this for initialization
    void Start () {
        bstate = BossState.Patrol;
        pstate = PhaseState.phase1;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        canattack = true;
        heal = spit = false;
        Attack = 0;
        buiscript = GetComponent<BossUIScript>();

	}
	
	// Update is called once per frame
	void Update () {

        if (YoungOne.activeSelf == true)
        {
            direction = YoungOne.GetComponent<Transform>().position - transform.position;
        }
        else if (OldOne.activeSelf == true)
        {
            direction = OldOne.GetComponent<Transform>().position - transform.position;
        }
        direction.y= 0;
        anim.enabled = !gmscript.GetComponent<GMScript>().timestop;
        switch (bstate)
        {
            case BossState.Patrol:
                if(gmscript.GetComponent<GMScript>().timestop==false)
                Patrol();
                break;
            case BossState.Battlemode:
                if (gmscript.GetComponent<GMScript>().timestop == false)
                Battlemode(direction);
                break;
            case BossState.Death:
                break;

        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            pstate = PhaseState.phase1;
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            pstate = PhaseState.phase2;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            pstate = PhaseState.phase3;
        }
        //Debug.Log("Boss State" + bstate.ToString());
        //Debug.Log("Lanter " + lanterncount);
        //Debug.Log(countattack);
        //Debug.Log("Canattack " + canattack);
    }



    public void changephase2()
    {
        pstate = PhaseState.phase2;
    }

    public void changephase3()
    {
        pstate = PhaseState.phase3;
    }

    void Patrol()
    {
        Vector3 dir;
        if (waypoints.Length> 0)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Running", true);
            if (Vector3.Distance(waypoints[currentWP].transform.position,
                transform.position)<accuracyWP)
            {
                currentWP++;
                if (currentWP >= waypoints.Length)
                {
                    currentWP = 0;
                }

            }

            dir = waypoints[currentWP].transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);
            //transform.Translate(0, 0, speed * Time.deltaTime);
            rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
        }
    }


    void Battlemode(Vector3 direction)
    {
       
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 
            rotSpeed * Time.deltaTime);


        if (direction.magnitude > 18)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            anim.SetBool("Running", true);
            anim.SetBool("Idle", false);
            anim.SetInteger("Attack",0);

        }
        else if(canattack==true)
        {
            if (pstate == PhaseState.phase1)
            {
                //phase1Attack();
                phase1Attack();
                canattack = false;
            }
            else if (pstate == PhaseState.phase2)
            {

                phase2Attack();
                canattack = false;
            }
            else
            {
                phase3Attack();
                canattack = false;
            }

            StartCoroutine(AttackCD());
        }
    }

    void phase1Attack()
    {  //attack 7
        Attack = Random.Range(1, 4);
        anim.SetInteger("Attack", Attack);
        anim.SetBool("Idle", true);
        anim.SetBool("Running", false);
        StartCoroutine(AttackCD());

    }


    void phase2Attack()
    {
        Attack = Random.Range(1,5);

        if (Attack == 4)
        {
            StartCoroutine(slam());
        }
        else
        {
            Attack = Random.Range(1,4);
        }

        anim.SetInteger("Attack", Attack);
        anim.SetBool("Idle", true);
        anim.SetBool("Running", false);
        StartCoroutine(AttackCD());
    }

    void phase3Attack()
    {

        Attack = Random.Range(1,7);
        if (Attack == 4)
        {
            StartCoroutine(slam());
        }
        else if (Attack == 5 && spit == false)
        {
            //summon spit
            Debug.Log("summon minions");
            skilleffects[1].SetActive(true);
            spit = true;
        }
        else if (Attack == 6 && heal == false)
        {

            buiscript.Heal();
            heal = true;
        }
        else
        {
            Attack = Random.Range(1, 4);
        }

        anim.SetInteger("Attack", Attack);
        anim.SetBool("Idle", true);
        anim.SetBool("Running", false);
        StartCoroutine(AttackCD());
    }

    IEnumerator CDskill()
    {
        yield return new WaitForSeconds(60);
        heal = false;
    }

    IEnumerator slam()
    { 

        yield return new WaitForSeconds(0.3f);
        GameObject obj = (GameObject)Instantiate(skilleffects[0], skillpos.transform.position, Quaternion.identity);
        
    }

    void GroundAttack()
    {

        //Destroy(obj,2f);
       // GameObject obj = (GameObject)Instantiate(Skilleffects[3], new Vector3(direct.x, direct.y + 2f, direct.z), Quaternion.identity);
        Debug.Log("Explosion");
    }

    IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(4f);
        Attack = 0;
        canattack = true;
        anim.SetBool("Idle", false);
        Debug.Log("ReadyToAttack");
    }

 


}
