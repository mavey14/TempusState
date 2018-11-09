using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardBoss : MonoBehaviour {

    [SerializeField]
    private Transform player;
    [HideInInspector]
    public Animator anim;

    public GameObject[] waypoints;
    int currentWP = 0;
    public float rotSpeed = 1f;
    float speed = 8f;
    float accuracyWP = 20f;
    float downtime;
    bool dt,canattack,ca;
    int countattack,lanterncount;
    AnimatorStateInfo charPlayerStateInfo;

    public enum BossState
    {
        Patrol,Battlemode,Destroybell,ChargeAttack,Death,Down
    }

    public BossState bstate;

    // Use this for initialization
    void Start () {
        bstate = BossState.Patrol;
        anim = GetComponent<Animator>();
        downtime = 5f;
        dt = ca = false;
        countattack = lanterncount = 0;
        canattack = true;
       
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = player.position - transform.position;
        direction.y= 0;
        
        switch (bstate)
        {
            case BossState.Patrol:
                Patrol();
                break;
            case BossState.Battlemode:
                Battlemode(direction);
                break;
            case BossState.Destroybell:
                break;
            case BossState.ChargeAttack:
                ChargeAttack();
                break;
            case BossState.Death:
                break;
            case BossState.Down:
                if (dt == false)
                {
                    Down();
                    dt = true;
                }
                break;
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            lanterncount++;
            Debug.Log("asdad");
        }
        //Debug.Log("Boss State" + bstate.ToString());
        //Debug.Log("Lanter " + lanterncount);
        //Debug.Log(countattack);
        //Debug.Log("Canattack " + canattack);
	}



    void ChargeAttack()
    {
        if (ca == false)
        {
            anim.SetTrigger("CU");
            ca = true;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && lanterncount <= 5)
        {
            ca = false;
            bstate = BossState.Battlemode;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f && lanterncount >= 5)
        {
            ca = false;
            bstate = BossState.Down;
        }
     
    }

    void Down()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Running", false);
        anim.SetInteger("Attack", 0);
        anim.SetTrigger("FC");
        StartCoroutine(Recover(downtime));
    }

    IEnumerator Recover(float downtime)
    {
        yield return new WaitForSeconds(downtime);
        anim.SetBool("Recover", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("Recover", false);
        dt = false;
        lanterncount = 0;
        bstate = BossState.Battlemode;
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
            transform.Translate(0, 0, speed * Time.deltaTime);
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
            anim.SetInteger("Attack",0);

        }
        else if(canattack==true)
        {
            //Debug.Log("test");
            //anim.SetBool("Running", false);
            //anim.SetInteger("Attack", 1);
            //countattack++;
            //if (countattack == 2)
            //{
            //    anim.SetBool("Running", false);
            //    anim.SetInteger("Attack", 0);
            //    countattack = 0;
            //    bstate = BossState.ChargeAttack;
            //}
            //StartCoroutine(attackinterval());
            //canattack = false;
            if (countattack >= 6)
            {
                anim.SetBool("Running", false);
                anim.SetInteger("Attack", 0);
                countattack = 0;
                bstate = BossState.ChargeAttack;
            }
            else
            {
                StartCoroutine(attackinterval());
                countattack++;
                 //Debug.Log("attackingboss");
                 canattack = false;
            }
                
           
        }
    }
    IEnumerator attackinterval()
    {
        anim.SetBool("Running", false);
        anim.SetInteger("Attack", 1);
        yield return new WaitForSeconds(.2f);
        anim.SetInteger("Attack",0);
        yield return new WaitForSeconds(3f);
        canattack = true;
    }



}
