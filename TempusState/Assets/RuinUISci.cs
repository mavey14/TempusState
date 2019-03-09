using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuinUISci : MonoBehaviour {

    [SerializeField]
    Image currenthb;


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
        updatehp();

        if (Input.GetKeyDown(KeyCode.L))
        {
            Damage(10f);
        }
	}

    public void Damage(float dmg)
    {
        hitpoints = hitpoints - dmg;
    }

    void updatehp()
    {
        if(currenthb)
            currenthb.fillAmount = hitpoints / maxhp;
    }
}
