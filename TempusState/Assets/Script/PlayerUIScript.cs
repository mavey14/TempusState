using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour {

    [SerializeField]
    Image currenthb;
    [SerializeField]
    Image currentmn;
    [SerializeField]
    Image currentstam;
    OldOneScript oldscript;
    PlayerScript pscript;
    [SerializeField]
    GameObject gmscript;
    [SerializeField]
    PlayerUIScript young;
    private static float hitpoints;
    private float maxhp;
    private static float manapoints;
    private float maxmana;
    private static float stamina;
    private float maxstamina;
    float staminaregentime;
    float staminadepletedskill1;
    float staminadepletedskill2;
    float staminadepletedskill3;
    // Use this for initialization
    void Start () {
        maxhp = 150;
        maxmana = 100;
        maxstamina = 100;
        hitpoints = this.gameObject.tag == "Old" ? young.hpts : maxhp;
        manapoints = this.gameObject.tag=="Old"?young.mnpts:maxmana;
        stamina = maxstamina;
        if (this.gameObject.tag == "Old")
        {
            oldscript = GetComponent<OldOneScript>();
        }
        else
        {
            pscript = GetComponent<PlayerScript>();
        }
        staminaregentime = 0.1f;
        staminadepletedskill1 = 10;
        staminadepletedskill1 = 20;
        staminadepletedskill1 = 30;
        //currenthb = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {
            Damage(20f);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            usemana(20f);
        }

        if (gmscript.GetComponent<GMScript>().cskill == 1)
        {
            DrainSkill1();
        }
        else if (gmscript.GetComponent<GMScript>().cskill == 2)
        {
            DrainSkill2();
        }
        else if (gmscript.GetComponent<GMScript>().cskill == 3)
        {
            DrainSkill3();
        }
        //Debug.Log(gmscript.GetComponent<GMScript>().timestop);
        updatehphpandmana();
       // Debug.Log(stamina);
        
        //if (!pscript.panim.GetCurrentAnimatorStateInfo(0).IsName("Dodge")&&stamina<100&&this.gameObject.tag=="Player")
        //{
        //    stamina += Time.deltaTime/staminaregentime;
        //    stamina = Mathf.Clamp(stamina, 0f, 100f);
        //}
    }

    

    void DrainSkill1()
    {

        manapoints -= 10 * Time.deltaTime;
        if (manapoints <= 0)
        {
            manapoints = 0;
            if (gameObject.tag == "Old")
            {
                Debug.Log("turnchild");
                oldscript.Transformtochild();
            }
              
        }
    }

    void DrainSkill2()
    {

        manapoints -= 20 * Time.deltaTime;
        if (manapoints <= 0)
        {
            manapoints = 0;
            if (this.gameObject.tag == "Player")
            {
                gmscript.GetComponent<GMScript>().timestop = false;
                pscript.turnoffeffects();
            }
           
           
        }
    }

    void DrainSkill3()
    {

        manapoints -= 20 * Time.deltaTime;
        if (manapoints <= 0)
        {
            manapoints = 0;
            if (this.gameObject.tag == "Player")
            {
               
                pscript.turnoffeffects3();
            }


        }
    }

    public void DodgeStamina()
    {
        stamina -= 50f;
    }

    public float stam
    {
        get
        {
            //Some other code
            return stamina;
        }
        set
        {
            //Some other code
            stamina = value;
        }
    }


    public float hpts
    {
        get
        {
            //Some other code
            return hitpoints;
        }
        set
        {
            //Some other code
            hitpoints = value;
        }
    }

    public float mnpts
    {
        get
        {
            //Some other code
            return manapoints;
        }
        set
        {
            //Some other code
            manapoints = value;
        }
    }

    public void Damage(float dmg)
    {
        hitpoints = hitpoints - dmg;
    }

    public void addmana()
    {
        if (gmscript.GetComponent<GMScript>().cskill == 0)
        manapoints +=5;
        if (manapoints > 100)
            manapoints = 100;
    }

    public void usemana(float manause)
    {
        manapoints -= manause;
    }

    void updatehphpandmana()
    {
        if(currenthb)
        currenthb.fillAmount = hitpoints / maxhp;
        if (currentmn)
            currentmn.fillAmount = manapoints / maxmana;
    }
}
