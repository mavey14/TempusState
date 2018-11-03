using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardBoss : MonoBehaviour {

    [SerializeField]
    private Transform player;
    Animator anim;

    public GameObject[] waypoints;
    int currentWP = 0;
    public float rotSpeed = 1f;
    float speed = 5f;
    float accuracyWP = 20f;

    public enum BossState
    {
        Patrol,Battlemode,Destroybell,ChargeAttack,Death
    }

    public BossState bstate;

    // Use this for initialization
    void Start () {
        bstate = BossState.Patrol;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = player.position - transform.position;
        direction.y= 0;

        switch (bstate)
        {
            case BossState.Patrol:
                Patrol(direction);
                break;
            case BossState.Battlemode:
                break;
            case BossState.Destroybell:
                break;
            case BossState.ChargeAttack:
                break;
            case BossState.Death:
                break;
        }
	}


    void Patrol(Vector3 direction)
    {
        Vector3 dir = direction;
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


}
