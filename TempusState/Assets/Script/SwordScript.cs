using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

    [SerializeField]
    private GameObject skel;
    [SerializeField]
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"  && skel.GetComponent<SkeletonScript>().noattack!=0 &&
            !player.GetComponent<PlayerScript>().panim.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
        {
            FindObjectOfType<Audiomanager>().Play("PlayerHit1");
            other.GetComponent<PlayerScript>().kb();
            other.GetComponent<PlayerUIScript>().Damage(5f);
        }
    }
}
