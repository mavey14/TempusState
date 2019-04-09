using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBossSCript : MonoBehaviour {

    [SerializeField]
    private GameObject YoungOne;
    [SerializeField]
    private GameObject OldOne;
    Vector3 direct;
    Vector3 fbdirect;
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
    [SerializeField]
    Transform Fireballpos;
    [SerializeField]
    GameObject gmscript;
    [SerializeField]
    GameObject[] Phase3Active;
    [SerializeField]
    CameraScript cm;
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
        anim.enabled = !gmscript.GetComponent<GMScript>().timestop;
        switch (bstate)
        {
            case BossState.idle:
                if (gmscript.GetComponent<GMScript>().timestop == false)
                    Idle();
                break;
            case BossState.battle:
                if (gmscript.GetComponent<GMScript>().timestop == false)
                    Battle(direct);
                break;
            case BossState.death:
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            resetanim();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("test");
            bstate = BossState.battle;
            // anim.SetBool("Idle", false);
            //anim.SetBool("Move", true);
        }
    }

    void resetanim()
    {
        canattack = true;
        anim.SetInteger("Attack", 0);
        anim.Play("Idle");
    }

    void Idle()
    {
        anim.SetBool("Idle", true);
    }

    void Battle(Vector3 direct)
    {
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation
            (direct), rotSpeed * Time.deltaTime);

        //if (direct.magnitude > 250) //change gaano kalayo ang player bago umuatak
        //{
        //    noattack = 0;
        //    //rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
        //    anim.SetBool("Idle",true);
        //    anim.SetInteger("Attack", noattack);
        //}
        //else
        //{
            if (canattack == true)
            {
                //int random = Random.Range(1,5);
                //noattack = random;
                anim.SetBool("Idle",false);
                Attack();
                canattack = false;
            }

        //}
    }

    void Attack()
    {
        //Vector3 player = Player.GetComponent<Transform>().position;
        if (pstate == PhaseState.phase1)
        {
            phase1attack();
        }
        else if (pstate == PhaseState.phase2)
        {
            // astate = (AttackState)Random.Range(1, 6);
            phase2attack();
        }
        else if (pstate == PhaseState.phase3)
        {
            // astate = (AttackState)Random.Range(1, 6);
            phase2attack();
            if (Phase3Active[0].activeSelf==false)
            {
                Phase3Active[0].SetActive(true);
                Phase3Active[1].SetActive(true);
            }
        }


    }

    void phase1attack()
    {
        int random = Random.Range(1,3);
        //Debug.Log(3);
        if (random == 1)
        {
            Fireball(random);
            anim.SetInteger("Attack", 1);
            anim.SetBool("Idle", true);
            StartCoroutine(AttackCD(2f));
            Debug.Log("asd");

        }
        else if (random == 2)
        {
            Meteor();
            anim.SetInteger("Attack",2);
            anim.SetBool("Idle", true);
            StartCoroutine(AttackCD(2f));
        }
    }

    void phase2attack()
    {
        //if (chargecount != 5)
        //{
        int random = Random.Range(1,5);
            if (random == 1)
            {
                Fireball(random);
                anim.SetInteger("Attack", random);
                anim.SetBool("Idle", true);
                StartCoroutine(AttackCD(2f));

            }
            else if (random == 2)
            {
                Meteor();
                anim.SetInteger("Attack", 3);
                anim.SetBool("Idle", true);
                StartCoroutine(AttackCD(2f));
            }
            else if (random ==3)
            {
                Explosion();
                anim.SetInteger("Attack", 4);
                anim.SetBool("Idle", true);
                StartCoroutine(AttackCD(2f));
            }
            else if (random == 4)
            {
                 GroundAttack();
                anim.SetInteger("Attack", 4);
                anim.SetBool("Idle", true);
                StartCoroutine(AttackCD(2f));
            }
        //}
        //else
        //{
        //    anim.SetInteger("Attack", 5);
        //    anim.SetBool("Idle", true);
        //    StartCoroutine(ChargeExplo());
        //    StartCoroutine(AttackCD(6f));
        //}

    }

    IEnumerator ChargeExplo()
    {
        yield return new WaitForSeconds(5f);
        chargecount = 0;

    }

    void GroundAttack()
    {

        //Destroy(obj,2f);
        FindObjectOfType<Audiomanager>().Play("FireBall");
        GameObject obj = (GameObject)Instantiate(Skilleffects[3], new Vector3(direct.x, direct.y+2f, direct.z), Quaternion.identity);
        Debug.Log("Explosion");
    }

    void Explosion()
    {

        //Destroy(obj,2f);
        FindObjectOfType<Audiomanager>().Play("FireBall");
        GameObject obj = (GameObject)Instantiate(Skilleffects[2], new Vector3(direct.x, direct.y, direct.z), Quaternion.identity);
        Debug.Log("Explosion");
    }

    void Meteor()
    {
        int random = Random.Range(0,4);
        if(random==1||random==2)
        {
            cm.camerashake = true;
        }
        FindObjectOfType<Audiomanager>().Play("FireBall");
        GameObject obj = (GameObject)Instantiate(Skilleffects[1], new Vector3(direct.x, direct.y+40f, direct.z), Quaternion.identity);
        Debug.Log("Meteor");
    }

    void Fireball(int random)
    {
        Debug.Log("aaasd");
        FindObjectOfType<Audiomanager>().Play("FireBall");
        GameObject obj = (GameObject)Instantiate(Skilleffects[0],Fireballpos.transform.position,Quaternion.identity);
        Vector3 direction = Fireballpos.transform.position - getposplayer();
        obj.transform.rotation = Quaternion.LookRotation(direction);
        Vector3 pos = YoungOne.GetComponent<Transform>().transform.position;
        //Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
       // bullet.velocity = (player.position - bullet.position).normalized * constant;
            obj.GetComponent<LavaAttack>().player = pos;
        Debug.Log("Fireball");
    }

    Vector3 getposplayer()
    {
        if (YoungOne.activeSelf == true)
        {
            direct = YoungOne.GetComponent<Transform>().transform.position - Fireballpos.transform.position;
        }
        if (OldOne.activeSelf == true)
        {
            direct = OldOne.GetComponent<Transform>().transform.position - Fireballpos.transform.position;
        }
        return direct;
    }

    IEnumerator AttackCD(float delay)
    {
        yield return new WaitForSeconds(delay);
        canattack = true;
        anim.SetBool("Idle", false);
        // Debug.Log("ReadyToAttack");
    }
}
