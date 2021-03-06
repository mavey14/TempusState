﻿using System.Collections;
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
    [SerializeField]
    Image[] cdimage;
    float cd;
    public bool iscd;
    private static float hitpoints;
    private float maxhp;
    private static float manapoints;
    private float maxmana;
    private static float stamina;
    private float maxstamina;
    float staminaregentime;
    float manadepletedskill1;
    float manadepletedskill2;
    float manadepletedskill3;

    // Use this for initialization
    void Start () {
        maxhp = 150;
        maxmana = 100;
        maxstamina = 100;
        hitpoints = this.gameObject.tag == "Old" ? young.hpts : maxhp;
        manapoints = this.gameObject.tag=="Old"?young.mnpts:maxmana;
        stamina = maxstamina;
        cd = 4f;
        if (this.gameObject.tag == "Old")
        {
            oldscript = GetComponent<OldOneScript>();
        }
        else
        {
            pscript = GetComponent<PlayerScript>();
        }
        staminaregentime = 0.1f;
        manadepletedskill1 = 10f;
        manadepletedskill2= 20f;
        manadepletedskill3 = 30f;
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

        if (this.gameObject.tag == "Player")
        {
            if (!pscript.panim.GetCurrentAnimatorStateInfo(0).IsName("Dodge") && stamina < 100)
            {
                stamina += Time.deltaTime / staminaregentime;
                stamina = Mathf.Clamp(stamina, 0f, 100f);
            }
        }

        if (iscd)
        {
            if (gameObject.tag == "Player")
            {
                cdimage[0].fillAmount += 1 / cd * Time.deltaTime;
                cdimage[1].fillAmount += 1 / cd * Time.deltaTime;
                cdimage[2].fillAmount += 1 / cd * Time.deltaTime;

                if (cdimage[0].fillAmount >= 1)
                {
                    cdimage[0].fillAmount = 0;
                    cdimage[1].fillAmount = 0;
                    cdimage[2].fillAmount = 0;
                    iscd = false;
                }
            }
           
        }
       
    }


    public void Poison()
    {
        hitpoints -= 0.5f * Time.deltaTime * 1;
    }
    

    void DrainSkill1()
    {

        manapoints -= manadepletedskill1 * Time.deltaTime;
        if (manapoints <= 0)
        {
            iscd = true;
            manapoints = 0;
            if (gameObject.tag == "Old")
            {
               // Debug.Log("turnchild");
               FindObjectOfType<Audiomanager>().Play("SkillAge");
                oldscript.Transformtochild();
            }
              
        }
    }

    void DrainSkill2()
    {
        Debug.Log("test2");
        manapoints -= manadepletedskill2 * Time.deltaTime;
        if (manapoints <= 0)
        {
            iscd = true;
            manapoints = 0;
            if (this.gameObject.tag == "Player")
            {
                gmscript.GetComponent<GMScript>().timestop = false;
                gmscript.GetComponent<GMScript>().cskill = 0;
                FindObjectOfType<Audiomanager>().Stop("SkillTimeStop");
                pscript.turnoffeffects();
            }
           
           
        }
    }

    void DrainSkill3()
    {

        manapoints -= manadepletedskill3 * Time.deltaTime;
        if (manapoints <= 0)
        {
            iscd = true;
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
        if(hitpoints>0)
        hitpoints = hitpoints - dmg;
        if (hitpoints <= 0)
        {
            pscript.dead();
        }
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
        if (currentstam)
            currentstam.fillAmount = stamina / maxstamina;
    }
}
