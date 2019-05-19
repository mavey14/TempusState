using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour {

    [SerializeField]
    private GameObject graveyardboss;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private BoxCollider collide;
    private void Start()
    {
        //collide.enabled = false;
    }

    private void Update()
    {
        collide.enabled = graveyardboss.GetComponent<GraveyardBoss>().anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") ||
            graveyardboss.GetComponent<GraveyardBoss>().anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") ||
            graveyardboss.GetComponent<GraveyardBoss>().anim.GetCurrentAnimatorStateInfo(0).IsName("ChargeAttack");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" &&
            graveyardboss.GetComponent<GraveyardBoss>().Attack>0&&
            !player.GetComponent<PlayerScript>().panim.GetCurrentAnimatorStateInfo(0).IsName("Dodge")&&player.GetComponent<PlayerScript>().backtrack==false)
        {
            //Debug.Log("Damage Player");
            other.GetComponent<PlayerUIScript>().Damage(2f);
        }
    }
}
