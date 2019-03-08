using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBossSCript : MonoBehaviour {

    [SerializeField]
    private GameObject YoungOne;
    [SerializeField]
    private GameObject OldOne;
    Vector3 direct;
    Animator anim;
    [SerializeField]
    GameObject[] Skilleffects;
    enum PhaseState { phase1, phase2, phase3 };
    enum BossState { idle, battle, death };
    PhaseState pstate;
    BossState bstate;
    public int chargecount;
    bool canattack;
    float rotSpeed;
    public int noattack;
    // Use this for initialization
    void Start () {
        pstate = PhaseState.phase1;
        anim = GetComponent<Animator>();
        //astate = null;
        canattack = true;
        direct = Vector3.zero;
        rotSpeed = 3f;
        noattack = 0;
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
        switch (bstate)
        {
            case BossState.idle:
                Idle();
                break;
            case BossState.battle:
                Battle(direct);
                break;
            case BossState.death:
                break;

        }

        

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("test");
            bstate = BossState.battle;
            // anim.SetBool("Idle", false);
            //anim.SetBool("Move", true);
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

        if (direct.magnitude > 100) //change gaano kalayo ang player bago umuatak
        {
            noattack = 0;
            //rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
            anim.SetBool("Idle", false);
            anim.SetInteger("Attack", noattack);
        }
        else
        {
            if (canattack == true)
            {
                //int random = Random.Range(1,5);
                //noattack = random;
                Attack();
                canattack = false;
            }

        }
    }

    void Attack()
    {
        //Vector3 player = Player.GetComponent<Transform>().position;
        if (pstate == PhaseState.phase1)
        {
            phase2attack();
        }
        else if (pstate == PhaseState.phase2)
        {
            // astate = (AttackState)Random.Range(1, 6);
            phase2attack();
        }


    }

    void phase1attack()
    {
        int random = Random.Range(1, 4);
        Debug.Log(random);
        if (random == 1 || random == 2)
        {
            Fireball();
            anim.SetInteger("Attack", random);
            anim.SetBool("Idle", true);
            StartCoroutine(AttackCD(3f));

        }
        else if (random == 3)
        {
            Meteor();
            anim.SetInteger("Attack", 3);
            anim.SetBool("Idle", true);
            StartCoroutine(AttackCD(4f));
        }
    }

    void phase2attack()
    {
        if (chargecount != 5)
        {
            int random = Random.Range(1, 5);
            if (random == 1 || random == 2)
            {
                Fireball();
                anim.SetInteger("Attack", random);
                anim.SetBool("Idle", true);
                StartCoroutine(AttackCD(3f));

            }
            else if (random == 3)
            {
                Meteor();
                anim.SetInteger("Attack", 3);
                anim.SetBool("Idle", true);
                StartCoroutine(AttackCD(4f));
            }
            else if (random == 4)
            {
                Explosion();
                anim.SetInteger("Attack", 4);
                anim.SetBool("Idle", true);
                StartCoroutine(AttackCD(4f));
            }
        }
        else
        {
            anim.SetInteger("Attack", 5);
            anim.SetBool("Idle", true);
            StartCoroutine(ChargeExplo());
            StartCoroutine(AttackCD(6f));
        }

    }

    IEnumerator ChargeExplo()
    {
        yield return new WaitForSeconds(5f);
        chargecount = 0;

    }

    void Explosion()
    {

        Debug.Log("Explosion");
    }

    void Meteor()
    {

        Debug.Log("Meteor");
    }

    void Fireball()
    {
        //GameObject obj = (GameObject)Instantiate(fbal, transform.position, new Quaternion());
        //Vector3 pos = Player.GetComponent<Transform>().transform.position;
        //obj.GetComponent<fball>().player = pos;
        // Instantiate(fbal, transform.position, Quaternion.identity);
        Debug.Log("Fireball");
    }

    IEnumerator AttackCD(float delay)
    {
        yield return new WaitForSeconds(delay);
        canattack = true;
        anim.SetBool("Idle", false);
        // Debug.Log("ReadyToAttack");
    }
}
