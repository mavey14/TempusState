using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject gmscript;
    [SerializeField]
    private BoxCollider axebox;
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gboss"  && 
             player.GetComponent<PlayerScript>().noclicks!=0)
        {
            other.GetComponent<BossUIScript>().Damage(15);
            player.GetComponent<PlayerUIScript>().addmana();
            if (other.GetComponent<GraveyardBoss>().bstate != GraveyardBoss.BossState.Battlemode)
                other.GetComponent<GraveyardBoss>().bstate = GraveyardBoss.BossState.Battlemode;
        }
        else if (other.tag == "Gboss" &&
             player.GetComponent<PlayerScript>().panim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"))
        {
            other.GetComponent<BossUIScript>().Damage(25);
            player.GetComponent<PlayerUIScript>().addmana();
            if (other.GetComponent<GraveyardBoss>().bstate != GraveyardBoss.BossState.Battlemode)
                other.GetComponent<GraveyardBoss>().bstate = GraveyardBoss.BossState.Battlemode;
        }

        if (other.tag == "Enemy" &&
             player.GetComponent<PlayerScript>().noclicks != 0)
        {
            other.GetComponent<SkeletonScript>().ReduceHP();

            player.GetComponent<PlayerUIScript>().addmana();
            
        }
        else if (other.tag == "Enemy" &&
             player.GetComponent<PlayerScript>().panim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"))
        {
            other.GetComponent<SkeletonScript>().ReduceHP();
            player.GetComponent<PlayerUIScript>().addmana();
            
        }

        if (other.tag == "la" &&
            player.GetComponent<PlayerScript>().noclicks != 0)
        {
            other.GetComponent<RuinUISci>().Damage(10f);

            player.GetComponent<PlayerUIScript>().addmana();

        }
        else if (other.tag == "la" &&
             player.GetComponent<PlayerScript>().panim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"))
        {
            other.GetComponent<RuinUISci>().Damage(20f);
            player.GetComponent<PlayerUIScript>().addmana();

        }

        if (other.tag == "lr" &&
           player.GetComponent<PlayerScript>().noclicks != 0)
        {
            other.GetComponent<RuinUISci>().Damage(10f);

            player.GetComponent<PlayerUIScript>().addmana();

        }
        else if (other.tag == "lr" &&
             player.GetComponent<PlayerScript>().panim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"))
        {
            other.GetComponent<RuinUISci>().Damage(20f);
            player.GetComponent<PlayerUIScript>().addmana();

        }

        if (other.tag == "wb" &&
           player.GetComponent<PlayerScript>().noclicks != 0)
        {
            other.GetComponent<RuinUISci>().Damage(10f);

            player.GetComponent<PlayerUIScript>().addmana();

        }
        else if (other.tag == "wb" &&
             player.GetComponent<PlayerScript>().panim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"))
        {
            other.GetComponent<RuinUISci>().Damage(20f);
            player.GetComponent<PlayerUIScript>().addmana();

        }

    }
}
