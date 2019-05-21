using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LavaUIScript : MonoBehaviour {

    [SerializeField]
    Image currenthb;
    [SerializeField]
    GameObject[] deactivateUI;
    LavaBossSCript lbscript;
    [SerializeField]
    GameObject roadToFloatIsland;
    private float hitpoints;
    private float maxhp;
    private float totalhp;
    // Use this for initialization
    void Start () {
        maxhp = 800;
        hitpoints = maxhp;
        lbscript = GetComponent<LavaBossSCript>();
    }
	
	// Update is called once per frame
	void Update () {
        updatehp();

        if (hitpoints >= 600)
        {
            lbscript.pstate = LavaBossSCript.PhaseState.phase1;
        }
        else if (hitpoints >= 400)
        {
            lbscript.pstate = LavaBossSCript.PhaseState.phase2;
        }
        else if (hitpoints >= 200)
        {
            lbscript.pstate = LavaBossSCript.PhaseState.phase3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            hitpoints -= 100;
        }


    }

    public void Damage(float dmg)
    {
        hitpoints = hitpoints - dmg;
        if (hitpoints <= 0)
        {
           lbscript.anim.SetTrigger("Death");

            foreach (var item in deactivateUI)
            {
                if (item != null)
                    item.SetActive(false);
            }
            if (roadToFloatIsland != null)
                roadToFloatIsland.SetActive(true);
           Destroy(gameObject,2f);

        }
    }

    void updatehp()
    {
        if (currenthb)
            currenthb.fillAmount = hitpoints / maxhp;
    }
}
