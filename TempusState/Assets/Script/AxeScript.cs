using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject gmscript;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy"  && 
             player.GetComponent<PlayerScript>().noclicks!=0)
        {
            other.GetComponent<BossUIScript>().Damage(other.GetComponent<GraveyardBoss>().bstate == GraveyardBoss.BossState.Down ? 
                gmscript.GetComponent<GMScript>().cskill == 0 ? 1:10 : gmscript.GetComponent<GMScript>().cskill == 1 ? 10 : 1);

            player.GetComponent<PlayerUIScript>().mnpts = player.GetComponent<PlayerUIScript>().mnpts>100 ? 5:0;
        }
        else if (other.tag == "Enemy" &&
             player.GetComponent<PlayerScript>().panim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"))
        {
            other.GetComponent<BossUIScript>().Damage(other.GetComponent<GraveyardBoss>().bstate == GraveyardBoss.BossState.Down ?
                gmscript.GetComponent<GMScript>().cskill == 0 ? 5 : 25 : gmscript.GetComponent<GMScript>().cskill == 1 ? 25:5);
            player.GetComponent<PlayerUIScript>().mnpts = player.GetComponent<PlayerUIScript>().mnpts > 100 ? 5 : 0;
        }
        
    }
}
