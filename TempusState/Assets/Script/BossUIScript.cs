using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIScript : MonoBehaviour {


    [SerializeField]
    Image currenthb;
    [SerializeField]
    GraveyardBoss GraveyardBossScript;
    [SerializeField]
    GameObject TeleportToFloat;
    private float hitpoints;
    private float maxhp;
    private float totalhp;
    // Use this for initialization
    void Start () {
        maxhp = 1000;
        hitpoints = maxhp;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Damage(100f);
            Debug.Log("Damage");
        }
        updatehp();
        if (hitpoints < 660&&GraveyardBossScript.pstate== GraveyardBoss.PhaseState.phase1)
        {
            GraveyardBossScript.changephase2();
            Debug.Log("change to phase 2");
        }
        else if (hitpoints < 330&& GraveyardBossScript.pstate == GraveyardBoss.PhaseState.phase2)
        {
            GraveyardBossScript.changephase3();
            Debug.Log("change to phase 3");
        }
    }


    public void Damage(float dmg)
    {
        hitpoints = hitpoints - dmg;
        if (hitpoints<=0)
        {
            GraveyardBossScript.anim.SetTrigger("Death");
            GraveyardBossScript.bstate = GraveyardBoss.BossState.Death;
            TeleportToFloat.SetActive(true);
            GMScript.stages[0] = true;
            Destroy(gameObject,0.5f);
        }
    }

    void updatehp()
    {
        if (currenthb)
            currenthb.fillAmount = hitpoints / maxhp;
        
    }

    public void Heal()
    {
        hitpoints += 200f;
    }
}
