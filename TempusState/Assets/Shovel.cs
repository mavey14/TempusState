using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour {

    [SerializeField]
    private GameObject graveyardboss;
    [SerializeField]
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" &&
            graveyardboss.GetComponent<GraveyardBoss>().
            anim.GetCurrentAnimatorStateInfo(0).IsName("AttackCombo")&&
            !player.GetComponent<PlayerScript>().panim.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
        {
            Debug.Log("Damage Player");
            other.GetComponent<PlayerUIScript>().Damage(5f);
        }
    }
}
